$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });

    GetTransfer("all");
    $('#displayshowroom').on('change', function () {
        var departmentid = this.value;
        if (departmentid == "---Select Showroom----") {
            GetTransfer("all");
        } else {
            //alert(departmentid);
            GetTransfer(departmentid);
        }
    })
})

var tableEmployee = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';

function GetTransfer(departmentId) {
    tableEmployee = $('#tableTransfer').DataTable({
        ajax: {
            url: (departmentId == "all") ? "/api/Transfer?showroomid=all" : "/api/Transfer?showroomid=" + departmentId,
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
                //     data: "expensetypeid"
                //},
                {
                    data: "amount"
                },

                {
                    data: "note"
                },

                //{
                //    data: "showroomid"
                //},

                //{
                //    data: "createby"
                //},
                //{
                //    data: "createdate"
                //},

            {
                data: "id",
                render: function (data) {
                    return "<button onclick='TransferEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'​>Edit</button>" + "<button onclick='TransferDelete(" + data + ")' class='btn btn-danger btn-xs' >Delete</button>";
                }
            },
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}

function TransferAction() {
    var action = '';
    action = document.getElementById('btnTransfer').innerText;
    if (action == "Add New") {
        $('#date').val(moment().format('YYYY-MM-DD'));
        document.getElementById('btnTransfer').innerText = "Save";
        document.getElementById('date').disabled = false;
        //document.getElementById('expensetypeid').disabled = false;
        document.getElementById('amount').disabled = false;
        document.getElementById('note').disabled = false;
        document.getElementById('showroomid').disabled = false;
        $("#expensetypeid").focus();
        $("#amount").val(0);
    } else if (action == "Save") {
        var response = Validate();
        if (response == false) {
            return false;
        }
        var data = new FormData();
        data.append("date", $("#date").val());
        //data.append("expensetypeid", $("#expensetypeid").val());
        data.append("amount", $("#amount").val());
        data.append("note", $("#note").val());
        data.append("showroomid", $("#showroomid").val());

        //console.log(files[0]);

        $.ajax({
            type: "POST",
            url: "/api/Transfer",
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {

                //console.log(result);

                toastr.success("Transfer has been created successfully.", "Server Response");
                tableEmployee.ajax.reload();
                $('#id').val(result.id);
                $('#transferModel').modal('hide');
                document.getElementById('btnTransfer').innerText = "Add New";
                $('#date').val('');
                //$('#expensetypeid').val('');
                $('#amount').val('0.00');
                $('#note').val('');

            },
            error: function (error) {
                //console.log(error);
                toastr.error("Transfer Already Exists!.", "Server Response");
            }
        });

        //maritalstatus

    } else if (action == "Update") {
        var response = Validate();
        if (response == false) {
            return false;
        }
        var data = new FormData();
        data.append("id", $('#id'));
        data.append("date", $("#date").val());
        //data.append("expensetypeid", $("#expensetypeid").val());
        data.append("amount", $("#amount").val());
        data.append("note", $("#note").val());
        data.append("showroomid", $("#showroomid").val());

        console.log(data);

        $.ajax({
            type: "PUT",
            url: "/api/Transfer/" + $('#id').val(),
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {

                //console.log(result);

                toastr.success("Transfer has been updated successfully.", "Server Response");
                tableEmployee.ajax.reload();

                //$('#employeeId').val(result.id);
                $('#transferModel').modal('hide');

                document.getElementById('btnTransfer').innerText = "Add New";
                $('#date').val('');
                //$('#expensetypeid').val('');
                $('#amount').val('0.00');
                $('#note').val('');

            },
            error: function (error) {
                //console.log(error);
                toastr.error("Transfer Already Exists!.", "Server Response");
            }
        });

    }
}

function TransferEdit(id) {
    document.getElementById('btnTransfer').innerText = "Update";

    $.ajax({
        url: "/api/Transfer/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#id').val(result.id);
            var pd = moment(result.date).format("YYYY-MM-DD");
            $('#date').val(pd);
            //$('#expensetypeid').val(result.expensetypeid);
            $("#amount").val(result.amount);
            $("#showroomid").val(result.showroomid);
            $('#note').val(result.note);
            //console.log(result);


            //Enable Control
            document.getElementById('date').disabled = false;
            //document.getElementById('expensetypeid').disabled = false;
            document.getElementById('amount').disabled = false;
            document.getElementById('note').disabled = false;
            document.getElementById('showroomid').disabled = false;


            $('#transferModel').modal('show');
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });

}

function TransferDelete(id) {
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
                    url: "/api/Transfer/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableEmployee.ajax.reload();
                        toastr.success("Transfer has been Deleted successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("This Transfer is already exists in Database", "Service Response");
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

function ClickAddnewTransfer() {
    document.getElementById('date').disabled = true;
    //document.getElementById('expensetypeid').disabled = true;
    document.getElementById('amount').disabled = true;
    document.getElementById('note').disabled = true;
    document.getElementById('showroomid').disabled = true;

    $('#amount').val('');
    $('#note').val('');
    $('#date').val('');
    $('#amount').focus();
    document.getElementById('btnTransfer').innerText = "Add New";
}
