$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });

    $('#emergencyModel').on('show.bs.modal', function () {

        //GetParrent(id);
    })
})

var tableSalary = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';

function GetEmergency(id) {
    ClearData();
    //console.log(id)
    tableSalary = $('#emergencyTable').DataTable({
        ajax: {
            url: "/api/EmergencyByStudent/" + id,
            dataSrc: ""
        },
        columns: [

            //{
            //    data: "emerid"
            //},
            //{
            //    data: "emerstudentid"
            //},
            {
                data: "name1"
            },
            {
                data: "contactnumber1"
            },
            {
                data: "relationship1"
            },
             {
                 data: "name2"
             },
            {
                data: "contactnumber2"
            },
            {
                data: "relationship2"
            },
            {
                data: "emerid",
                render: function (data) {
                    return "<button onclick='EmergencyEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>" + "<button onclick='EmergencyDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });

    DisableControl();
}

function EmergencyAction() {
    var action = '';
    action = document.getElementById('btnEmergency').innerText;
    if (action == "Add New") {
        document.getElementById('btnEmergency').innerText = "Save";
        EnableControl();
        $('#name1').focus();

    } else if (action == "Save") {
        if ($('#name1').val().trim() == "") {
            $('#name1').css('border-color', 'red');
            $('#name1').focus();
            toastr.info('Please enter Contact Person 1', "Server Response")
        } else if ($('#contactnumber1').val().trim() == "") {
            $('#contactnumber1').css('border-color', 'red');
            $('#contactnumber1').focus();
            toastr.info('Please enter Contact Phone Number 1', "Server Response")
        } else {
            $('#contactnumber1').css('border-color', '#cccccc');
            $('#name1').css('border-color', '#cccccc');
            var dataSave = {
                emerstudentid: $('#emerstudentid').val(),
                name1: $('#name1').val(),
                contactnumber1: $('#contactnumber1').val(),
                relationship1: $('#relationship1').val(),
                name2: $('#name2').val(),
                contactnumber2: $('#contactnumber2').val(),
                relationship2: $('#relationship2').val(),

            };

            console.log(dataSave);

            $.ajax({
                url: "/api/Emergencys",
                data: JSON.stringify(dataSave),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Emergency has been created successfully.", "Server Response");
                    tableSalary.ajax.reload();
                    DisableControl();
                    ClearData();
                },
                error: function (errormessage) {
                    toastr.error("This Emergency is already exists in Database", "Service Response");
                }
            })
        }
    } else if (action == "Update") {
        if ($('#name1').val().trim() == "") {
            $('#name1').css('border-color', 'red');
            $('#name1').focus();
            toastr.info('Please enter Contact Person 1', "Server Response")
        } else if ($('#contactnumber1').val().trim() == "") {
            $('#contactnumber1').css('border-color', 'red');
            $('#contactnumber1').focus();
            toastr.info('Please enter Contact Phone Number 1', "Server Response")
        } else {
            $('#contactnumber1').css('border-color', '#cccccc');
            $('#name1').css('border-color', '#cccccc');
        
            var data = {
                emerid: $('#emerid').val(),
                emerstudentid: $('#emerstudentid').val(),
                name1: $('#name1').val(),
                contactnumber1: $('#contactnumber1').val(),
                relationship1: $('#relationship1').val(),
                name2: $('#name2').val(),
                contactnumber2: $('#contactnumber2').val(),
                relationship2: $('#relationship2').val(),

            };

            //console.log(data);

            $.ajax({
                url: "/api/Emergencys/" + data.emerid,
                data: JSON.stringify(data),
                type: "PUT",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Emergency has been update successfully.", "Server Response");
                    tableSalary.ajax.reload();
                    DisableControl();
                    ClearData();

                },
                error: function (errormessage) {
                    toastr.error("This Emergency Save Not Complete!", "Service Response");
                }
            })
        }
    }

}

function EmergencyEdit(Id) {
    //alert(id);
    $.ajax({
        url: "/api/Emergencys/" + Id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            ClearData();
            $('#emerid').val(result.emerid);
            $('#emerstudentid').val(result.emerstudentid);
            $('#name1').val(result.name1);
            $('#contactnumber1').val(result.contactnumber1);
            $('#relationship1').val(result.relationship1);
            $('#name2').val(result.name2);
            $('#contactnumber2').val(result.contactnumber2);
            $('#relationship2').val(result.relationship2);
            document.getElementById('btnEmergency').innerText = "Update";
            //alert(result.Id);
            EnableControl();
            $('#name1').focus();
        },
        error: function (errormessage) {
            toastr.error("This Emergency have no in Database", "Service Response");
        }
    });

}

function EmergencyDelete(id) {
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
                    url: "/api/Emergencys/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableSalary.ajax.reload();
                        toastr.success("Emergency has been Deleted successfully!", "Service Response");
                        DisableControl();
                    },
                    error: function (errormessage) {
                        toastr.error("This Emergency Cannot Delete from Database!", "Service Response");

                    }
                });
            }
        }
    });

}

function DisableControl() {
    document.getElementById('name1').disabled = true;
    document.getElementById('contactnumber1').disabled = true;
    document.getElementById('relationship1').disabled = true;
    document.getElementById('name2').disabled = true;
    document.getElementById('contactnumber2').disabled = true;
    document.getElementById('relationship2').disabled = true;
    document.getElementById('btnEmergency').innerText = "Add New";
}

function EnableControl() {
    document.getElementById('name1').disabled = false;
    document.getElementById('contactnumber1').disabled = false;
    document.getElementById('relationship1').disabled = false;
    document.getElementById('name2').disabled = false;
    document.getElementById('contactnumber2').disabled = false;
    document.getElementById('relationship2').disabled = false;
}

function ClearData() {
    $('#name1').val('');
    $('#contactnumber1').val('');
    $('#relationship1').val('');
    $('#name2').val('');
    $('#contactnumber2').val('');
    $('#relationship2').val('');

}