/**
 * @version 2.0.0
 * @since 09-08-2016
 * @author Armindo Maurits
 * JS for our Docker Dashboard
 */

/**
 * The string[] where all alive node IP-addresses will be saved.
 * @type {Array}
 */
var nodes = [];
/**
 * This var is used to save the instanced DataTable object in. Is used in multiple functions.
 */
var myTableDataTable;
/**
 * Starting URL for all requests, based on the website the user opens.
 * @example {string} "http://192.168.1.199:8080/ictlab/api"
 * @type {string}
 */
const host = "http://145.24.222.227:8080/ictlab/api";
var alertDiv = $('.alert');

$(function () {
    /**
     * Initialize a new DataTable object on the given HTML table
     * @return {Object} appends the action buttons to the HTML TableRow after the JSON is loaded
     */
    myTableDataTable = $('#myTable').DataTable({
        "ajax": {
            "url": "/Home/GetContainers",
            "dataSrc": ""
        },
        "columns": [
            { "data": "Id" },
            { "data": "Name" },
            { "data": "CreationDate" },
            { "data": "State" },
            {
                "render": function () {
                    return '<span class="glyphicon glyphicon-play" data-command="start" data-toggle="tooltip" title="Start container"></span><span class="glyphicon glyphicon-stop" data-toggle="tooltip"  data-command="stop" title="Stop container"></span><span class="glyphicon glyphicon-repeat" data-command="restart" data-toggle="tooltip" title="Restart container"></span> <span class="glyphicon glyphicon-remove-sign" data-command="delete" data-toggle="tooltip" title="Delete container"></span><span class="glyphicon glyphicon-export" data-command="move" data-toggle="tooltip" title="Move container"></span><span class="glyphicon glyphicon-duplicate" data-command="scale" data-toggle="scale" title="Scale container"></span>';
                }
            }
        ],
        "order": [[0, "asc"]]
    });

    $("#myTable tbody").on("click", "span", function () {
        var command = $(this).attr('data-command');

        if (command === "move" || command === "scale") {
            moveOrScale($(this), command);
        } else {
            //Command start, stop, restart, delete
            startRequest($(this), command);
        }
    });

    /**
    * Show our model when the newContainer button is clicked.
    */
    $('#newContainer').click(function () {
        $('#newContainerModal').modal('show');
    });

    /**
     * Start a new container create request with filled in details
     */
    $('#startNewContainer').click(function () {      
        var containerName = $('#newContainerName').val();
        var node = $('#startingNode option:selected').text();
        var baseImage = $('#newContainerBaseImage').val();
        var hostPort = $('#hostPort').val();
        var containerPort = $('#containerPort').val();
        var containerData = {
            containerName: containerName,
            node: node,
            baseImage: baseImage,
            hostPort: hostPort,
            containerPort: containerPort
        };

        $.ajax({
            type: "POST",
            url: '/Home/PostCreateContainer',
            data: containerData,
            dataType: 'json',
            statusCode: {
                201: function () {
                    showWarningMessage("Created container: " + containerName);

                    reloadDataTable(3000);
                },
                503: function (data) {
                    showWarningMessage("Could not create container: " + containerName);
                }
            }
        });

    });

    /**
    * Pass through the given Node, id and method when the moveContainer button is clicked.
    */
    $('#moveContainer').click(function () {
        var node = $('#moveContainerNodes option:selected').text();
        var id = $('#moveContainerId').val();
        var method = $('#moveContainer').val();

        postMoveOrScaleContainer(id, node, method);
    });

    getNumberOfNodes();
})

/**
* Adds a node to the HTML DOM element lists. So these Node IP-addresses can be selected on the Modal.
* @param value - The IP-address to be added to the dropdown lists.
*/
function addNode(value) {
    $('#startingNode, #moveContainerNodes').append($('<option>').text(value).attr('value', value));
}

/**
* Gets the number of nodes from the Controller by JSON GET request and passes these individual IP-addresses to the addNode function to render them on screen.
*/
function getNumberOfNodes() {
    $.ajax({
        url: '/Home/GetNodes',
        type: 'GET',
        dataType: 'json',
        success: function (json) {
            $.each(json, function (i, node) {
                addNode(node);
            });
        }
    });
}

/**
* Shows the moveContainerModal and fills in the ID and clicked command.
*/
function moveOrScale(currentObject, command) {
    var rowData = myTableDataTable.row(currentObject.parents('tr')).data();
    var id = rowData.Id;

    $('#moveContainerModal').modal('show');
    $('#moveContainerId').val(id);
    $('#moveContainer').val(command);
}

/**
* Gets a JSON at given URL. This causes the given container to be moved or scaled to given Docker Node.
* @param {number} id - ID of the desired Docker Container, which has to be moved/scaled.
* @param {string} node - Target Docker Node IP-address.
* @param {string} method - Move or scale.
*/
function postMoveOrScaleContainer(id, node, method) {
    var url = host + "/containers/" + id + "/" + method + "/" + node;

    $.ajax({
        type: "GET",
        dataType: "json",
        url: url
    });
}

/**
* Start a request on given container and afterwards changes the background color of this row by given result status.
* @param {object} currentObject - Table row Object which is clicked.
* @param {string} command - The command which has the be executed on this Container. Can be "start", "stop", "restart", "delete".
*/
function startRequest(currentObject, command) {
    var parentTableRow = currentObject.parents('tr');
    var rowData = myTableDataTable.row(parentTableRow).data();

    $.ajax({
        type: 'POST',
        url: '/Home/PostAction',
        data: { actionName: command, id: rowData.Id },
        dataType: 'json',
        statusCode: {
            201: function () {
                showWarningMessage(command + " container " + rowData.Id);

                parentTableRow.css("background-color", "green");
            },
            503: function () {
                showWarningMessage("Could not " + command + " container: " + rowData.Id);

                parentTableRow.css("background-color", "red");
            }
        }
    });

    reloadDataTable(4000);
}

/**
 * Reload the datatable after given milliseconds.
 * @param {number} ms - number of milliseconds.
 */
function reloadDataTable(ms) {
    setTimeout(function () {
        myTableDataTable.ajax.reload();
    }, ms);
}

/**
* Shows the information panel at the top of the screen when a message needs to be shown.
* @param {string} messageText - The information message which needs to be shown to the user.
*/
function showWarningMessage(messageText) {
    $('.alert').text(messageText);
    $('.alert').show();
}