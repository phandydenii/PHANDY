$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });

    GetCollectmoney("0");
    $('#displayemployee').on('change', function () {
        var empId = this.value;
        if (empId == "---អ្នកដឹកទាំងអស់----") {
            GetCollectmoney("0");
        } else {
            //alert(departmentid);
            GetCollectmoney(empId);
        }
    })
})

var tableEmployee = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';

function GetCollectmoney(empId) {
    $('#date').val(moment().format('YYYY-MM-DD'));

    tableEmployee = $('#tableCollectmoney').DataTable({
        ajax: {
            url: (empId == "0") ? "/api/CollectMoney?empId=0" : "/api/CollectMoney?empId=" + empId,
            dataSrc: ""
        },
        columns: [
                //{
                //    data: "id"
                //},
                {
                    data: "date",
                    render: function (data) {
                        return moment(new Date(data)).format('DD-MMM-YYYY');
                    }
                },
                //{
                //     data: "employeeid"
                //},
                {
                    data: "namekh"
                },
                {
                    data: "amount"
                },

                {
                    data: "deliveryin"
                },

                {
                    data: "deliveryout"
                },
                 {
                    data: "bonus"
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
                    return "<button onclick='CollectmoneyEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'​>Edit</button>" + "<button onclick='CollectmoneyDelete(" + data + ")' class='btn btn-danger btn-xs' >Delete</button>" + "<button onclick='PrintCollectMoney(" + data + ")' class='btn btn-success btn-xs' style='margin-left:5px;'​>Print</button>";
                }
            },
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}

function CollectMondeyAction() {
    var chkPaid = 0;
    var action = '';
    action = document.getElementById('btnCollectmoney').innerText;
    if (action == "Add New") {
        document.getElementById('btnDeliveryin').disabled = false;
        document.getElementById('btnDeliveryout').disabled = false;
        $('#date').val(moment().format('YYYY-MM-DD'));
        document.getElementById('btnCollectmoney').innerText = "Save";
        document.getElementById('date').disabled = false;
        document.getElementById('employeeid').disabled = false;
        document.getElementById('amount').disabled = false;
        document.getElementById('deliveryin').disabled = false;
        document.getElementById('deliveryout').disabled = false;
        document.getElementById('bonus').disabled = false;
        document.getElementById('status').disabled = false;
        $("#amount").val('0.00')
        $("#deliveryin").val('0.00')
        $("#deliveryout").val('0.00')
        $("#bonus").val('0.00')
        $("#employeeid").focus();
        
    } else if (action == "Save") {
        if ($("#status").is(':checked')) {
            chkPaid = 1;
        } else {
            chkPaid = 0;
        }
        var response = Validate();
        if (response == false) {
            return false;
        }
        var data = new FormData();
        data.append("date", $("#date").val());
        data.append("employeeid", $("#employeeid").val());
        data.append("amount", $("#amount").val());
        data.append("deliveryin", $("#deliveryin").val());
        data.append("deliveryout", $("#deliveryout").val());
        data.append("bonus", $("#bonus").val());
        data.append("status", chkPaid);
        //alert(chkPaid);
        //console.log(files[0]);

        $.ajax({
            type: "POST",
            url: "/api/CollectMoney",
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {

                //console.log(result);

                toastr.success("Collect Money has been created successfully.", "Server Response");
                tableEmployee.ajax.reload();
                $('#id').val(result.id);
                $('#collectmoneyModel').modal('hide');
                document.getElementById('btnCollectmoney').innerText = "Add New";
                $('#date').val('');
                //$('#expensetypeid').val('');
                $('#amount').val('0.00');
                $('#deliveryin').val('0.00');
                $('#deliveryout').val('0.00');
                $('#bonus').val('0.00');
                $('#status').val(false);
            },
            error: function (error) {
                //console.log(error);
                toastr.error("This shipper already paid cannot update!.", "Server Response");
            }
        });

        //maritalstatus

    } else if (action == "Update") {
        if ($("#status").is(':checked')) {
            chkPaid = 1;
        } else {
            chkPaid = 0;
        }
        var response = Validate();
        if (response == false) {
            return false;
        }
        var data = new FormData();
        data.append("date", $("#date").val());
        data.append("employeeid", $("#employeeid").val());
        data.append("amount", $("#amount").val());
        data.append("deliveryin", $("#deliveryin").val());
        data.append("deliveryout", $("#deliveryout").val());
        data.append("bonus", $("#bonus").val());
        data.append("status", chkPaid);

        console.log(data);

        $.ajax({
            type: "PUT",
            url: "/api/CollectMoney/" + $('#id').val(),
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {

                //console.log(result);

                toastr.success("Collect Money has been updated successfully.", "Server Response");
                tableEmployee.ajax.reload();

                //$('#employeeId').val(result.id);
                $('#collectmoneyModel').modal('hide');
                document.getElementById('btnCollectmoney').innerText = "Add New";
                $('#date').val('');
                $('#amount').val('0.00');
                $('#deliveryin').val('0.00');
                $('#deliveryout').val('0.00');
                $('#bonus').val('0.00');
                $('#status').val(false);

            },
            error: function (error) {
                //console.log(error);
                toastr.error("This shipper cannot update!.", "Server Response");
            }
        });

    }
}

function CollectmoneyEdit(id) {
    document.getElementById('btnCollectmoney').innerText = "Update";

    $.ajax({
        url: "/api/CollectMoney/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#id').val(result.id);
            var pd = moment(result.date).format("YYYY-MM-DD");
            $('#date').val(pd);
            $('#employeeid').val(result.employeeid).change();
            $("#amount").val(result.amount);
            $("#deliveryin").val(result.deliveryin);
            $('#deliveryout').val(result.deliveryout);
            $("#bonus").val(result.bonus);
            $("#status").prop('checked', result.status > 0)

            //console.log(result);
           

            //Enable Control
            document.getElementById('date').disabled = false;
            document.getElementById('employeeid').disabled = false;
            document.getElementById('amount').disabled = false;
            document.getElementById('deliveryin').disabled = false;
            document.getElementById('deliveryout').disabled = false;
            document.getElementById('bonus').disabled = false;
            document.getElementById('status').disabled = false;

            $('#collectmoneyModel').modal('show');
            

            
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });

}

function CollectmoneyDelete(id) {
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
                    url: "/api/CollectMoney/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableEmployee.ajax.reload();
                        toastr.success("Collect Money has been Deleted successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.warning("Wrong Date or This shipper already paid!", "Service Response");
                    }
                });
            }
        }
    });
}

function Validate() {
    var isValid = true;
    if ($('#amount').val().trim() == "") {
        $('#amount').css('border-color', 'red');
        $('#amount').focus();
        isValid = false;
    } else {
        $('#amount').css('border-color', '#cccccc');
        $('#amount').focus();
    }
    return isValid;
}

function ClickAddnewCollectmoney() {
    document.getElementById('date').disabled = true;
    document.getElementById('employeeid').disabled = true;
    document.getElementById('amount').disabled = true;
    document.getElementById('deliveryin').disabled = true;
    document.getElementById('deliveryout').disabled = true;
    document.getElementById('bonus').disabled = true;
    document.getElementById('status').disabled = true;

    $('#amount').val('');
    $('#deliveryin').val('');
    $('#deliveryout').val('');
    $('#bonus').val('');
    $('#amount').focus();
    document.getElementById('btnDeliveryin').disabled = true;
    document.getElementById('btnDeliveryout').disabled = true;

    document.getElementById('btnCollectmoney').innerText = "Add New";
}


$('#employeeid').change(function () {
    var selectedDate = $('#date').val();
    var id = $('#employeeid').val();
    GetAmount(selectedDate, id);

});

$('input[name=date]').change(function () {
    var selectedDate = $('#date').val();
    var id = $('#employeeid').val();
    GetAmount(selectedDate,id);
});

function GetAmount(selectedDate, id) {
    //alert(selectedDate);
    //alert(id);
        $.ajax({
            url: "/api/CollectMoney?selectedDate=" + selectedDate + "&id=" + id,
            type: "GET",
            contentType: "application/json;charset=utf-8",
            datatype: "json",
            success: function (result) {
                if (result[0] == null) {
                    $("#amount").val('0.00');
                    $("#deliveryin").val('0.00');
                    $('#deliveryout').val('0.00');
                } else {
                    $("#amount").val(result[0].totalPrice);
                    $("#deliveryin").val(result[0].totalIn);
                    $('#deliveryout').val(result[0].totalOut);
                }
               
                //console.log(result);
                //Enable Control
                document.getElementById('date').disabled = false;
                document.getElementById('employeeid').disabled = false;
                document.getElementById('amount').disabled = false;
                document.getElementById('deliveryin').disabled = false;
                document.getElementById('deliveryout').disabled = false;
                //document.getElementById('bonus').disabled = false;
                //document.getElementById('status').disabled = false;

                //$('#collectmoneyModel').modal('show');
            },
            error: function (errormessage) {
                toastr.error("No Record Select!", "Service Response");
            }
        });
}

function PrintCollectMoney(id) {
    window.open("/collectmoney-invoice/" + id, "_self")
}

function LoadDeliveryin(createdate, employeeid, status) {
    var createdate = $('#date').val();
    var employeeid = $('#employeeid').val();
    var status = "ដឹក";
    deliveryTable = $('#deliveryTable').DataTable({
        ajax: {
            url: "/api/CollectMoney?createdate=" + createdate + "&employeeid=" + employeeid + "&status=" + status,
            type: "GET",
            contentType: "application/json;charset=utf-8",
            datatype: "json",
            dataSrc: ""
        },
        columns: [
            //{
            //    data: "id"
            //},
            //{
            //    data: "invoiceid"
            //},
            {
                data: "location",
            },
            //{
            //    data: "productname",
            //},
            //{
            //    data: "employeename",
            //},
            {
                data: "receiverphone"
            },
            {
                data: "price"
            },
            {
                data: "carprice"
            },
            {
                data: "shipprice"
            },
            {
                data: "status"
            },
             {
                 data: "deliverytype",
             },
            //{
            //    data: "createdate"
            //},
            //{
            //    data: "id",
            //    render: function (data) {
            //        return "<button onclick='InvoicedetailEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>" + "<button onclick='InvoicedetailDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
            //    }
            //}
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}

function LoadDeliveryout(createdate, employeeid, status) {
    var createdate = $('#date').val();
    var employeeid = $('#employeeid').val();
    var status = "យក";
    deliveryTable = $('#deliveryTable').DataTable({
        ajax: {
            url: "/api/CollectMoney?createdate=" + createdate + "&employeeid=" + employeeid + "&status=" + status,
            type: "GET",
            contentType: "application/json;charset=utf-8",
            datatype: "json",
            dataSrc: ""
        },
        columns: [
            //{
            //    data: "id"
            //},
            //{
            //    data: "invoiceid"
            //},
            {
                data: "location",
            },
            //{
            //    data: "productname",
            //},
            //{
            //    data: "employeename",
            //},
            {
                data: "receiverphone"
            },
            {
                data: "price"
            },
            {
                data: "carprice"
            },
            {
                data: "shipprice"
            },
            {
                data: "status"
            },
             {
                 data: "deliverytype",
             },
            //{
            //    data: "createdate"
            //},
            //{
            //    data: "id",
            //    render: function (data) {
            //        return "<button onclick='InvoicedetailEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>" + "<button onclick='InvoicedetailDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
            //    }
            //}
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}

