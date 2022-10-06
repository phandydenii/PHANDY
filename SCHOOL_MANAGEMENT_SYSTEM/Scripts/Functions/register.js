$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });
    GetRegister();
    //$('#registerModel').on('show.bs.modal', function () {

    //    //GetRegister();
    //})
})

var tableParrent = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';

function GetRegister() {
    //alert("Hello");
    tableParrent = $('#tableRegister').DataTable({
        ajax: {
            url: "/api/Registerstudents",
            dataSrc: ""
        },
        columns: [

            //{
            //    data: "id"
            //},
            {
                data: "registerid", render: function (data) {

                    return "R" + ("000000" + data).slice(-6);
                }
            },
            {
                data: "date",
                render: function (data) {
                    return moment(new Date(data)).format('DD-MMM-YYYY');
                }
            },
            //{
            //    data: "userid"
            //},
            {
                data: "fullname"
            },
            {
                data: "shiftname"
            },
            {
                data: "language"
            },
            {
                data: "period"
            },
            {
                data: "gradename"
            },
            {
                data: "registerfile",
                render: function (data) {
                    //console.log(data);
                    if (data != null && data !="") {
                        return "<a target='_blank' href='/Images/" + data + "'>Link File</a>";
                    } else {
                        return "<a target='_blank' href='/Images/no_pdf_file.pdf'>Link File</a>";
                    }
                }
            },
            {
                data: "id",
                render: function (data) {
                    return "<button onclick='RegisterEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>" + "<button onclick='RegisterDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });

    
    //DisableControl();
}

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#registerfile').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function registerAction() {
    var action = '';
    action = document.getElementById('btnRegister').innerText;
    if (action == "Add New") {
        //EnableControl();
        document.getElementById('btnRegister').innerText = "Save";
        $('#date').val(moment().format('YYYY-MM-DD'))
        getMaxInvNo();
    } else if (action == "Save") {
        var data = new FormData();
        var files = $("#filereg").get(0).files;
        //alert(files.length);
        if (files.length > 0) {
            data.append("registerfile", files[0]);
        }

        data.append("registerid", $("#registerid").val());
        data.append("date", $("#date").val());
        data.append("studentid", $("#studentid").val());
        data.append("shiftid", $("#shiftid").val());
        data.append("languageid", $("#languageid").val());
        data.append("periodid", $("#periodid").val());
        data.append("gradeid", $("#gradeid").val());
        data.append("registerfile", $("#registerfile").val());
        data.append("status", $("#status").val());

        //console.log(files[0]);

        $.ajax({
            type: "POST",
            url: "/api/Registerstudents",
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {

                //console.log(result);

                toastr.success("Register has been created successfully.", "Server Response");
                tableParrent.ajax.reload();

                $('#id').val(result.id);
                $('#registerModel').modal('hide');

                document.getElementById('btnRegister').innerText = "Add New";
                //DisableControl();
                ClearData();

            },
            error: function (error) {
                console.log(error);
                toastr.warning("Please check Student Name again!.", "Server Response");
            }
        });
        
    } else if (action == "Update") {
        var data = new FormData();
        var files = $("#filereg").get(0).files;
        //alert(files.length);
        if (files.length > 0) {
            data.append("registerfile", files[0]);
        }

        data.append("id", $("#id").val());
        data.append("registerid", $("#registerid").val());
        data.append("date", $("#date").val());
        data.append("studentid", $("#studentid").val());
        data.append("shiftid", $("#shiftid").val());
        data.append("languageid", $("#languageid").val());
        data.append("periodid", $("#periodid").val());
        data.append("gradeid", $("#gradeid").val());
        //data.append("registerfile", $("#registerfile").val());
        data.append("status", $("#status").val());
        data.append("file_old", $("#file_old").val());
        //console.log(files[0]);

        $.ajax({
            type: "PUT",
            url: "/api/Registerstudents/" + $('#id').val(),
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {

                //console.log(result);

                toastr.success("Register has been update successfully.", "Server Response");
                tableParrent.ajax.reload();

                $('#id').val(result.id);
                $('#registerModel').modal('hide');

                document.getElementById('btnRegister').innerText = "Add New";
                //DisableControl();
                ClearData();

            },
            error: function (error) {
                console.log(error);
                toastr.warning("Please check Student Name again!.", "Server Response");
            }
        });
    }

}

function RegisterEdit(id) {
    //EnableControl();
    $('#registerModel').modal('show');
    $.ajax({
        url: "/api/Registerstudents/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#id').val(result.id);
            $('#registerid').val(result.registerid);
            var payno = "R" + ("000000" + result.registerid).slice(-6);
            $('#regid').val(payno);
            var dr = moment(result.date).format("YYYY-MM-DD");
            $("#date").val(dr);
            $('#userid').val(result.userid);
            $('#studentid').val(result.studentid).change();
            $('#shiftid').val(result.shiftid);
            $('#languageid').val(result.languageid);
            $('#periodid').val(result.periodid);
            $('#gradeid').val(result.gradeid);
            $('#status').val(result.status);
            //$('#registerfile').val(result.registerfile);
            $('#file_old').val(result.registerfile);

            //console.log(result);

            document.getElementById('btnRegister').innerText = "Update";
            $('#studentid').focus();
        },
        error: function (errormessage) {
            toastr.error("No Register in Database", "Service Response");
        }
    });

}

function RegisterDelete(id) {
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
                    url: "/api/Registerstudents/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableParrent.ajax.reload();
                        toastr.success("Register has been Deleted successfully!", "Service Response");

                    },
                    error: function (errormessage) {
                        toastr.error("This Register Cannot Delete from Database!", "Service Response");

                    }
                });
            }
        }
    });

}

function DisableControl() {
    document.getElementById('registerid').disabled = true;
    document.getElementById('date').disabled = true;
    document.getElementById('userid').disabled = true;
    //document.getElementById('studentid').disabled = true;
    document.getElementById('shiftid').disabled = true;
    document.getElementById('languageid').disabled = true;
    document.getElementById('periodid').disabled = true;
    document.getElementById('gradeid').disabled = true;
    document.getElementById('status').disabled = true;
    document.getElementById('btnRegister').innerText = "Add New";
}

function EnableControl() {
    document.getElementById('registerid').disabled = false;
    document.getElementById('date').disabled = false;
    document.getElementById('userid').disabled = false;
    //document.getElementById('studentid').disabled = false;
    document.getElementById('shiftid').disabled = false;
    document.getElementById('languageid').disabled = false;
    document.getElementById('periodid').disabled = false;
    document.getElementById('gradeid').disabled = false;
    document.getElementById('status').disabled = false;
}

function ClearData() {
    $('#registerid').val('');
    $('#date').val('');
    //$('#userid').val('');
    //$('#studentid').val('');
    //$('#shiftid').val('');
    //$('#languageid').val('');
    //$('#periodid').val('');
    //$('#gradeid').val('');
    //$('#status').val(''); 
}

function ClickAddnewRegister() {
    //DisableControl();
    ClearData();
    //$('#').attr('src', '../Images/no_image.png');

}

function getMaxInvNo() {
    $.get("/api/Registerstudents/?a=1&b=2", function (data, status) {
        // alert("Data: " + data + "\nStatus: " + status);
        var arrayData = data.split(',');
        //alert(arrayData[0]);
        //alert(arrayData[1]);
        $('#registerid').val(arrayData[0]);
        $('#regid').val(arrayData[1]);

    });
}
