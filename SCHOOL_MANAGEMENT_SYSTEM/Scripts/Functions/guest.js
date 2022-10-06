$(document).ready(function () {
    //$('#GuestModal').on('show.bs.modal', function () {
    //});
    GetGuest();

});

var tableGuest = [];
function GetGuest() {
    

    tableGuest = $('#tableGuest').DataTable({
        ajax: {
            url: "/api/Guests",
            dataSrc: ""
        },
        columns: [
            {
                data: "id",
            },
            {
                data: "name"
            },
            {
                data: "namekh"
            },
            {
                data: "sex"
            },
            {
                data: "dob",
                render: function (data) {
                    return moment(new Date(data)).format('DD-MMM-YYYY');
                }
            },
            {
                data: "address"
            },
            {
                data: "phone"
            },
            {
                data: "email"
            },
            
            
            {
                data: "id",
                render: function (data) {
                    return "<button onclick='GuestEdit(" + data + ")' class='btn btn-warning btn-xs' style='border-width: 0px; width: 65px; margin-right: 5px;'><span class='glyphicon glyphicon-edit'></span> Edit</button>" +
                        "<button onclick='GuestDelete(" + data + ")' class='btn btn-danger btn-xs' style='border-width: 0px;'><span class='glyphicon glyphicon-trash'></span> Delete</button>";
                },
                "width": "130px"
            }
        ],
        destroy: true,
        "order": [[0, "desc"]],
        "info": false
    });
}


function GuestAction() {
    var action = '';
    action = document.getElementById('btnSaveGuest').innerText;

    if (action == "Add New") {
        document.getElementById('btnSaveGuest').innerText = 'Save';
        EnableControlGuest();

        $('#fname').focus();
        $('#dob').val(moment().format('YYYY-MM-DD'));
    }

    if (action === "Save") {
        //Validate();
        var data = {
            firstname: $('#fname').val(),
            lastname: $('#lname').val(),
            fullname: $('#fullname').val(),
            sex: $('#gender').val(),
            dob: $('#dob').val(),
            address: $('#address').val(),
            nationality: $('#nationality').val(),
            phone: $('#phone').val(),
            email: $('#email').val(),
            note: $('#note').val(),
            status: $('#status').val(),
        };
        $.ajax({
            url: "/api/Guests",
            data: JSON.stringify(data),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                toastr.success("Save to database successfully.", "Server Response");
                $('#tableGuest').DataTable().ajax.reload();

                $('#GuestModal').modal('hide');
                document.getElementById('btnSaveGuest').innerText = "Add New";
                
                //document.getElementById('btnSaveGuest').innerHTML = "<span class='glyphicon glyphicon-plus-sign'></span> <span class='kh'>បង្កើតថ្មី</span> / Add New";

            },
            error: function (errormessage) {
                toastr.error("This Guest is already exists.", "Server Response");
                //document.getElementById('btnSaveGuest').innerHTML = "<span class='glyphicon glyphicon-plus-sign'></span> <span class='kh'>បង្កើតថ្មី</span> / Add New";

            }
        });
    }
    else if (action === "Update") {
        //alert('hi');
        var data = {
            id: $('#Guestid').val(),
            firstname: $('#fname').val(),
            lastname: $('#lname').val(),
            fullname: $('#fullname').val(),
            sex: $('#gender').val(),
            dob: $('#dob').val(),
            address: $('#address').val(),
            nationality: $('#nationality').val(),
            phone: $('#phone').val(),
            email: $('#email').val(),
            note: $('#note').val(),
            status: $('#status').val(),
        };
        $.ajax({
            url: "/api/Guests/" + data.id,
            data: JSON.stringify(data),
            type: "PUT",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                toastr.success("Updated successfully.", "Server Response");
                $('#tableGuest').DataTable().ajax.reload();
                document.getElementById('btnSaveGuest').innerText = "Add New";
                ClearControlGuest();
               
                $('#GuestModal').modal('hide');
                //tableGuests.ajax.reload();

            },
            error: function (errormessage) {
                toastr.error("This Guest can't Update.", "Server Response");

            }
        });
    }
}


function GuestEdit(id) {
    ClearControlGuest();
    EnableControlGuest();

    //$('#status').val('');
    action = document.getElementById('btnSaveGuest').innerText = "Update";

    $.ajax({
        url: "/api/Guests/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            $('#Guestid').val(result.id);

            $('#fname').val(result.firstname);
            $('#lname').val(result.lastname);
            $('#fullname').val(result.fullname);
            $('#gender').val(result.sex);
            var dr = moment(result.dob).format("YYYY-MM-DD");
            $("#dob").val(dr);
            $('#address').val(result.address);
            $('#nationality').val(result.nationality);
            $('#phone').val(result.phone);
            $('#email').val(result.email);
            $('#note').val(result.note);
            $('#status').val(result.status);

            $("#GuestModal").modal('show');
        },
        error: function (errormessage) {
            toastr.error("Something unexpected happen.", "Server Response");
        }
    });
    return false;
}


function GuestDelete(id) {
    //alert('hi');
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
            //alert(id);
            if (result) {
                $.ajax({
                    url: "/api/Guests/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        $('#tableGuest').DataTable().ajax.reload();

                        toastr.success("Guest Deleted successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("Guest Can't be Deleted", "Service Response");
                    }
                });
            }
        }
    });
}



//function () {
//    document.getElementById('fname').disabled = true;
//    document.getElementById('lname').disabled = true;
//    document.getElementById('fullname').disabled = true;
//    document.getElementById('gender').disabled = true;
//    document.getElementById('dob').disabled = true;
//    document.getElementById('address').disabled = true;
//    document.getElementById('nationality').disabled = true;
//    document.getElementById('phone').disabled = true;
//    document.getElementById('email').disabled = true;
//    document.getElementById('note').disabled = true;
//    document.getElementById('status').disabled = true;


//}

//function EnableControlGuest() {
//    document.getElementById('fname').disabled = false;
//    document.getElementById('lname').disabled = false;
//    document.getElementById('fullname').disabled = false;
//    document.getElementById('gender').disabled = false;
//    document.getElementById('dob').disabled = false;
//    document.getElementById('address').disabled = false;
//    document.getElementById('nationality').disabled = false;
//    document.getElementById('phone').disabled = false;
//    document.getElementById('email').disabled = false;
//    document.getElementById('note').disabled = false;
//    document.getElementById('status').disabled = false;
//}

//function ClearControlGuest() {
//    $('#fname').val('');
//    $('#lname').val('');
//    $('#fullname').val('');
//    $('#gender').val('');
//    $('#dob').val('');
//    $('#address').val('');
//    $('#nationality').val('');
//    $('#phone').val('');
//    $('#email').val('');
//    $('#gender').val('');
//    $('#note').val('');
//    $('#status').val('');
//}

//function AddnewGuestAction() {
//    document.getElementById('btnSaveGuest').innerText = "Add New";
//    ();
//    ClearControlGuest();
//}
