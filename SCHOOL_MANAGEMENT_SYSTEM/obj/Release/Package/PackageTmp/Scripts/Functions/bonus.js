$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });

    $('#bonusModel').on('show.bs.modal', function () {
        //GetBonus(id);
    })
})

var tableBranch = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';

function GetBonus(id) {
    tableBranch = $('#bonusTable').DataTable({
        ajax: {
            url: "/api/BonusByEmployee/" + id,
            dataSrc: ""
        },
        columns: [
            //{
            //    data:"id"
            //},
            {
                data: "date",
                render: function (data) {
                    return moment(new Date(data)).format('DD-MMM-YYYY');
                }
            },
            {
                data: "type"
            },
            //{
            //    data: "employeeid"
            //},
            {
                data: "amount"
            },
            {
                data: "note"
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
                    return "<button onclick='BonusEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>" + "<button onclick='BonusDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}

function BonusAction() {
    var action = '';
    action = document.getElementById('btnBonus').innerText;
    if (action == "Add New") {
        $('#date').val(moment().format('YYYY-MM-DD'));
        document.getElementById('btnBonus').innerText = "Save";
        document.getElementById('date').disabled = false;
        document.getElementById('type').disabled = false;
        //document.getElementById('employeeid').disabled = false;
        document.getElementById('amount').disabled = false;
        document.getElementById('note').disabled = false;
        $('#employeeid').focus();

    } else if (action == "Save") {
        if ($('#amount').val().trim() == "") {
            $('#amount').css('border-color', 'red');
            $('#amount').focus();
            toastr.info('Please enter Amount.', "Server Response")
        } else {
            $('#amount').css('border-color', '#cccccc');

            var dataSave = {
                date: $('#date').val(),
                type: $('#type').val(),
                employeeid: $('#employeeid').val(),
                amount: $('#amount').val(),
                note: $('#note').val()
            };

            $.ajax({
                url: "/api/Bonus",
                data: JSON.stringify(dataSave),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Record has been created successfully.", "Server Response");
                    tableBranch.ajax.reload();
                    document.getElementById('btnBonus').innerText = "Add New";
                    $('#date').val('');
                    //$('#employeeid').val(''),
                    $('#type').val('');
                    $('#amount').val('');
                    $('#note').val('');
                    document.getElementById('date').disabled = true;
                    document.getElementById('type').disabled = true;
                    //document.getElementById('employeeid').disabled = true;
                    document.getElementById('amount').disabled = true;
                    document.getElementById('note').disabled = true;
                },
                error: function (errormessage) {
                    toastr.error("This Record is already exists in Database", "Service Response");
                }
            })
        }
    } else if (action == "Update") {
        if ($('#amount').val().trim() == "") {
            $('#amount').css('border-color', 'red');
            $('#amount').focus();
            toastr.info('Please enter Amount.', "Server Response")
        } else {
            $('#amount').css('border-color', '#cccccc');
            
            var data = {
                bonusid: $('#bonusid').val(),
                date: $('#date').val(),
                type: $('#type').val(),
                employeeid: $('#employeeid').val(),
                amount: $('#amount').val(),
                note: $('#note').val()
            };

            //console.log(data);

            $.ajax({
                url: "/api/Bonus/" + data.bonusid,
                data: JSON.stringify(data),
                type: "PUT",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Record has been update successfully.", "Server Response");
                    tableBranch.ajax.reload();
                    document.getElementById('btnBonus').innerText = "Add New";
                    $('#date').val('');
                    $('#type').val('');
                    //$('#employeeid').val(''),
                    $('#amount').val('');
                    $('#note').val('');
                    document.getElementById('date').disabled = true;
                    document.getElementById('type').disabled = true;
                   // document.getElementById('employeeid').disabled = true;
                    document.getElementById('amount').disabled = true;
                    document.getElementById('note').disabled = true;
                },
                error: function (errormessage) {
                    toastr.error("This Record is already exists in Database", "Service Response");
                }
            })
        }
    }

}

function BonusEdit(id) {
    //alert(id);
    $.ajax({
        url: "/api/Bonus/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#bonusid').val(result.id);
            var pd = moment(result.date).format("YYYY-MM-DD");
            $('#date').val(pd);
            $('#type').val(result.type);
            $('#employeeid').val(result.employeeid),
            $('#amount').val(result.amount);
            $('#note').val(result.note);
            document.getElementById('btnBonus').innerText = "Update";
            document.getElementById('date').disabled = false;
            document.getElementById('type').disabled = false;
            //document.getElementById('employeeid').disabled = false;
            document.getElementById('amount').disabled = false;
            document.getElementById('note').disabled = false;
            $('#amount').focus();
            
        },
        error: function (errormessage) {
            toastr.error("This Record is already exists in Database", "Service Response");
        }
        
    });

}

function BonusDelete(id) {
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
            id = $('#bonusid').val();
            if (result) {
                $.ajax({
                    url: "/api/Bonus/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableBranch.ajax.reload();
                        toastr.success("Record has been Deleted successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("This Rcord is already use in Database", "Service Response");
                    }
                });
            }
        }
    });
}