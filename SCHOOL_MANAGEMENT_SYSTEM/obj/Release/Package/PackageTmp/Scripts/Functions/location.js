$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });

    $('#locationModel').on('show.bs.modal', function () {

        GetLocation();
        document.getElementById('location').disabled = true;
        document.getElementById('btnLocation').innerText = "Add New";

    })
})

var tableLocation = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';

function GetLocation() {
    //alert('Hello');
    tableLocation = $('#locationTable').DataTable({
        ajax: {
            url: "/api/Location",
            dataSrc: ""
        },
        columns: [
            //{
            //    data:"id"
            //},
            {
                data: "location"
            },
            {
                data: "id",
                render: function (data) {
                    return "<button onclick='locationEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>" + "<button onclick='locationDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}

function LocationAction() {
    var action = '';
    action = document.getElementById('btnLocation').innerText;
    if (action == "Add New") {
        document.getElementById('btnLocation').innerText = "Save";
        document.getElementById('location').disabled = false;
        $('#location').focus();

    } else if (action == "Save") {
        if ($('#location').val().trim() == "") {
            $('#location').css('border-color', 'red');
            $('#location').focus();
            toastr.info('Please enter Department Name.', "Server Response")
        } else {
            $('#location').css('border-color', '#cccccc');

            var dataSave = {
                location: $('#location').val()
            };

            $.ajax({
                url: "/api/Location",
                data: JSON.stringify(dataSave),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Location has been created successfully.", "Server Response");
                    tableLocation.ajax.reload();
                    document.getElementById('btnLocation').innerText = "Add New";
                    $('#location').val('');
                    document.getElementById('location').disabled = true;
                },
                error: function (errormessage) {
                    toastr.error("This Location is already exists in Database", "Service Response");
                }
            })
        }
    } else if (action == "Update") {
        if ($('#location').val().trim() == "") {
            $('#location').css('border-color', 'red');
            $('#location').focus();
            toastr.info('Please enter Category Name.', "Server Response")
        } else {
            $('#location').css('border-color', '#cccccc');

            var data = {
                id: $('#id').val(),
                location: $('#location').val()
            };

            //console.log(data);

            $.ajax({
                url: "/api/Location/" + data.id,
                data: JSON.stringify(data),
                type: "PUT",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Location has been update successfully.", "Server Response");
                    tableLocation.ajax.reload();
                    document.getElementById('btnLocation').innerText = "Add New";
                    $('#location').val('');
                    document.getElementById('location').disabled = true;
                },
                error: function (errormessage) {
                    toastr.error("This Location is already exists in Database", "Service Response");
                }
            })
        }
    }

}

function locationEdit(id) {
    $.ajax({
        url: "/api/Location/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#id').val(result.id);
            $('#location').val(result.location);
            document.getElementById('btnLocation').innerText = "Update";
            document.getElementById('location').disabled = false;
            $('#location').focus();
        },
        error: function (errormessage) {
            toastr.error("This Location is already exists in Database", "Service Response");
        }
    });

}

function locationDelete(id) {
    bootbox.confirm({
        title: "",
        message: "Are you sure want to delete this?",
        button: {
            cancel: {
                label: "Cancel",
                ClassName: "btn-default",
            },
            confirm: {
                label: "Delete",
                ClassName: "btn-danger"
            }
        },
        callback: function (result) {
            if (result) {
                $.ajax({
                    url: "/api/Location/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableLocation.ajax.reload();
                        toastr.success("Location has been Deleted successfully!", "Service Response");
                        document.getElementById('location').disabled = true;
                        document.getElementById('btnLocation').innerText = "Add New";
                    },
                    error: function (errormessage) {
                        toastr.error("This Location cannot delete it already use in Database", "Service Response");
                    }
                });
            }
        }
    });
}