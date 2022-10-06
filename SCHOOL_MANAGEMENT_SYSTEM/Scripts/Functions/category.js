$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });

    $('#categoryModel').on('show.bs.modal', function () {

        GetCategory();
        document.getElementById('categoryname').disabled = true;
        document.getElementById('btnCategory').innerText = "Add New";

    })
})

var tableDepartment = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';

function GetCategory() {
    //alert('Hello');
    tableDepartment = $('#categoryTable').DataTable({
        ajax: {
            url: "/api/Category",
            dataSrc: ""
        },
        columns: [
            //{
            //    data:"id"
            //},
            {
                data: "categoryname"
            },
            {
                data: "id",
                render: function (data) {
                    return "<button onclick='categoryEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>" + "<button onclick='categoryDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}

function CategoryAction() {
    var action = '';
    action = document.getElementById('btnCategory').innerText;
    if (action == "Add New") {
        document.getElementById('btnCategory').innerText = "Save";
        document.getElementById('categoryname').disabled = false;
        $('#categoryname').focus();

    } else if (action == "Save") {
        if ($('#categoryname').val().trim() == "") {
            $('#categoryname').css('border-color', 'red');
            $('#categoryname').focus();
            toastr.info('Please enter Department Name.', "Server Response")
        } else {
            $('#categoryname').css('border-color', '#cccccc');

            var dataSave = {
                categoryname: $('#categoryname').val()
            };

            $.ajax({
                url: "/api/Category",
                data: JSON.stringify(dataSave),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Category has been created successfully.", "Server Response");
                    tableDepartment.ajax.reload();
                    document.getElementById('btnCategory').innerText = "Add New";
                    $('#categoryname').val('');
                    document.getElementById('categoryname').disabled = true;
                },
                error: function (errormessage) {
                    toastr.error("This Category is already exists in Database", "Service Response");
                }
            })
        }
    } else if (action == "Update") {
        if ($('#categoryname').val().trim() == "") {
            $('#categoryname').css('border-color', 'red');
            $('#categoryname').focus();
            toastr.info('Please enter Category Name.', "Server Response")
        } else {
            $('#categoryname').css('border-color', '#cccccc');

            var data = {
                id: $('#id').val(),
                categoryname: $('#categoryname').val()
            };

            //console.log(data);

            $.ajax({
                url: "/api/Category/" + data.id,
                data: JSON.stringify(data),
                type: "PUT",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Category has been update successfully.", "Server Response");
                    tableDepartment.ajax.reload();
                    document.getElementById('btnCategory').innerText = "Add New";
                    $('#categoryname').val('');
                    document.getElementById('categoryname').disabled = true;
                },
                error: function (errormessage) {
                    toastr.error("This Category is already exists in Database", "Service Response");
                }
            })
        }
    }

}

function categoryEdit(id) {
    $.ajax({
        url: "/api/Category/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#id').val(result.id);
            $('#categoryname').val(result.categoryname);
            document.getElementById('btnCategory').innerText = "Update";
            document.getElementById('categoryname').disabled = false;
            $('#categoryname').focus();
        },
        error: function (errormessage) {
            toastr.error("This Category is already exists in Database", "Service Response");
        }
    });

}

function categoryDelete(id) {
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
                    url: "/api/Category/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableDepartment.ajax.reload();
                        toastr.success("Category has been Deleted successfully!", "Service Response");
                        document.getElementById('categoryname').disabled = true;
                        document.getElementById('btnCategory').innerText = "Add New";
                    },
                    error: function (errormessage) {
                        toastr.error("This Category cannot delete it already use in Database", "Service Response");
                    }
                });
            }
        }
    });
}