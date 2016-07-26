//JS for our Docker dashboard
var nodes = [];
var myTableDataTable;
const host = "http://145.24.222.227:8080/ictlab/api";

//Doc ready function
$(function () {
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
        var url = host + "/containers/";
        var containerData = {
            containerName: containerName,
            node: node,
            baseImage: baseImage,
            hostPort: hostPort,
            containerPort: containerPort
        };

        $.ajax({
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            url: url,
            data: JSON.stringify(containerData),
            statusCode: {
                201: function () {
                    $('.alert').text("Created container: " + containerName);
                    reloadDataTable(3000);
                },
                503: function () {
                    $('.alert').text("Could not create container: " + containerName);
                }
            }
        });

    });

    $('#moveContainer').click(function () {
        var node = $('#moveContainerNodes option:selected').text();
        var id = $('#moveContainerId').val();
        var method = $('#moveContainer').val();

        postMoveOrScaleContainer(id, node, method);
    });

    getNumberOfNodes();
})

function addNode(value) {
    $('#startingNode, #moveContainerNodes').append($('<option>').text(value).attr('value', value));
}

function getNumberOfNodes() {
    var url = host + "/nodes/";

    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        success: function (json) {
            $.each(json, function (i, node) {
                addNode(node[1]);
            });
        }
    });
}

function moveOrScale(currentObject, command) {
    var rowData = myTableDataTable.row(currentObject.parents('tr')).data();
    var id = rowData[0];

    $('#moveContainerModal').modal('show');
    $('#moveContainerId').val(id);
    $('#moveContainer').val(command);

}

function postMoveOrScaleContainer(id, node, method) {
    var url = host + "/containers/" + id + "/" + method + "/" + node;

    $.ajax({
        type: "GET",
        dataType: "json",
        url: url
    });
}

function startRequest(currentObject, command) {
    var parentTableRow = currentObject.parents('tr');
    var rowData = myTableDataTable.row(parentTableRow).data();
    var id = rowData.Id;

    $.ajax({
        type: 'POST',
        url: '/Home/StartAction',
        data: JSON.stringify({ id: id, action: command }),
        dataType: 'html',
        contentType: "application/json; charset=utf-8",
        statusCode: {
            201: function () {
                parentTableRow.css("background-color", "green");
            },
            503: function () {
                parentTableRow.css("background-color", "red");
            }
        }
    });

    reloadDataTable(3000);
}

/**
 * Reload the datatable after given milliseconds
 * @param {number} ms
 */
function reloadDataTable(ms) {
    setTimeout(function () {
        myTableDataTable.ajax.reload();
    }, ms);
}