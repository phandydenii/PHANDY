$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });

    GetProduct("all");
    $('#displayshowroom').on('change', function () {
        var departmentid = this.value;
        if (departmentid == "---Select Showroom----") {
            GetProduct("all");
        } else {
            //alert(departmentid);
            GetProduct(departmentid);
        }
    })
})

var tableEmployee = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';

function GetProduct(departmentId) {
    tableEmployee = $('#tableProduct').DataTable({
        ajax: {
            url: (departmentId == "all") ? "/api/Product?showroomid=all" : "/api/Product?showroomid=" + departmentId,
            dataSrc: ""
        },
        columns: [
                //{
                //    data: "id"
                //},
                 {
                     data: "productname"
                 },
                 //{
                 //    data: "categoryid"
                 //},
                 // {
                 //     data: "showroomid"
                 // },
               {
                   data: "qtyonhand"
               },
              
                {
                    data: "photo"
                },
                
                {
                    data: "status"
                },

                //{
                //    data: "createby"
                //},
                //{
                //    data: "createdate"
                //},

            {
                data: "id",
                render: function (data) {
                    return "<button onclick='ProductEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'​>Edit</button>" + "<button onclick='ProductDelete(" + data + ")' class='btn btn-danger btn-xs' >Delete</button>";
                }
            },
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#photo').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function ProductAction() {
    var action = '';
    action = document.getElementById('btnProduct').innerText;
    if (action == "Add New") {
        document.getElementById('btnProduct').innerText = "Save";
        document.getElementById('productname').disabled = false;
        document.getElementById('categoryid').disabled = false;
        document.getElementById('qtyonhand').disabled = false;
        document.getElementById('photo').disabled = false;
        document.getElementById('showroomid').disabled = false;
        $('#photo').attr('src', '../Images/no_product.png');
        $("#productname").focus();
        $("#qtyonhand").val(0);
    } else if (action == "Save") {
        var response = Validate();
        if (response == false) {
            return false;
        }
        var data = new FormData();
        var files = $("#file").get(0).files;
        if (files.length > 0) {
            data.append("photo", files[0]);
        }

        data.append("productname", $("#productname").val());
        data.append("categoryid", $("#categoryid").val());
        data.append("qtyonhand", $("#qtyonhand").val());
        data.append("showroomid", $("#showroomid").val());
        
        //console.log(files[0]);

        $.ajax({
            type: "POST",
            url: "/api/Product",
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {

                //console.log(result);

                toastr.success("Product has been created successfully.", "Server Response");
                tableEmployee.ajax.reload();

                $('#id').val(result.id);
                $('#productModel').modal('hide');

                document.getElementById('btnProduct').innerText = "Add New";
                DisableControl();
                ClearData();

            },
            error: function (error) {
                //console.log(error);
                toastr.error("Product Already Exists!.", "Server Response");
            }
        });

        //maritalstatus

    } else if (action == "Update") {
        var response = Validate();
        if (response == false) {
            return false;
        }
        var data = new FormData();
        var files = $("#file").get(0).files;
        if (files.length > 0) {
            data.append("photo", files[0]);
        }
        data.append("id", $('#id'));
        data.append("productname", $("#productname").val());
        data.append("categoryid", $("#categoryid").val());
        data.append("qtyonhand", $("#qtyonhand").val());
        data.append("showroomid", $("#showroomid").val());
        data.append("file_old", $("#file_old").val());

        console.log(data);

        $.ajax({
            type: "PUT",
            url: "/api/Product/" + $('#id').val(),
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {

                //console.log(result);

                toastr.success("Product has been updated successfully.", "Server Response");
                tableEmployee.ajax.reload();

                //$('#employeeId').val(result.id);
                $('#productModel').modal('hide');

                document.getElementById('btnProduct').innerText = "Add New";
                DisableControl();
                ClearData();

            },
            error: function (error) {
                //console.log(error);
                toastr.error("Product Already Exists!.", "Server Response");
            }
        });

    }
}

function ProductEdit(id) {
    ClearData();
    EnableControl();
    document.getElementById('btnProduct').innerText = "Update";

    $.ajax({
        url: "/api/Product/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#id').val(result.id);
            $('#productname').val(result.productname);
            $('#categoryid').val(result.categoryid);
            $("#showroomid").val(result.showroomid);
            $("#qtyonhand").val(result.qtyonhand);
            $('#file_old').val(result.photo);
            //console.log(result);
            //alert(result.photo);
            if (result.photo == "") {
                $('#photo').attr('src', '../Images/no_product.png');
            } else {
                //alert(result.img);
                $('#photo').attr('src', '../Images/' + result.photo);
            }

            //Enable Control
            document.getElementById('productname').disabled = false;
            document.getElementById('categoryid').disabled = false;
            document.getElementById('showroomid').disabled = false;
            document.getElementById('qtyonhand').disabled = false;
            

            $('#productModel').modal('show');
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });

}

function ProductDelete(id) {
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
                    url: "/api/Product/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableEmployee.ajax.reload();
                        toastr.success("Product has been Deleted successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("This Product is already exists in Database", "Service Response");
                    }
                });
            }
        }
    });
}

function Validate() {
    var isValid = true;
    if ($('#productname').val().trim() == "") {
        $('#productname').css('border-color', 'red');
        $('#productname').focus();
        isValid = false;
    } else {
        $('#productname').css('border-color', '#cccccc');
        $('#productname').focus();
    }
    return isValid;
}

function ClickAddnewProduct() {
    document.getElementById('productname').disabled = true;
    document.getElementById('categoryid').disabled = true;
    document.getElementById('showroomid').disabled = true;
    document.getElementById('qtyonhand').disabled = true;
    
    $('#productname').val('');
    $('#categoryid').val('');
    $('#showroomid').val('');
    $('#qtyonhand').val('');
    
    $('#productname').focus();
    $('#photo').attr('src', '../Images/no_product.png');
    document.getElementById('btnProduct').innerText = "Add New";
}
