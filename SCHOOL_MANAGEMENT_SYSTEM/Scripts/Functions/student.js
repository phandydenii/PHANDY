$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });

    
    GetStudent("all");
    $('#displaybranch').on('change', function () {
        var branchid = this.value;
        if (branchid == "---Select Branch----") {
            GetStudent("all");
        } else {
            //alert(departmentid);
            GetStudent(branchid);
        }
    })
})

var tableEmployee = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';

function GetStudent(branchId) {
    tableEmployee = $('#tableStudent').DataTable({
        ajax: {
            url: (branchId == "all") ? "/api/Students?branchid=all" : "/api/Students?branchid=" + branchId,
            dataSrc: ""
        },
        columns: [
                //{
                //    data: "id"
                //},
                 {
                     data: "studentid", render: function (data) {

                         return "SH" + ("000000" + data).slice(-6);
                     }
                 },
                 //{
                 //    data: "studentbranchid"
                 //},
                 // {
                 //     data: "surname"
                 // },
                 //  {
                 //      data: "firstname"
                 //  },
                   {
                       data: "fullname"
                   },
                 {
                     data: "fullnamekh"
                 },
                 {
                     data: "studentgender"
                 },
                  {
                        data: "studentdob",
                        render: function (data) {
                            return moment(new Date(data)).format('DD-MMM-YYYY');
                        }
                  },
                   {
                       data: "studentpob"
                   },
                 //  {
                 //      data: "nationality"
                 //  },
                 //{
                 //    data: "nativelanguage"
                 //},
                 // {
                 //     data: "otherspoken"
                 // },
                 //  {
                 //      data: "schoolyear"
                 //  },
                 //  {
                 //      data: "studentphoto"
                 //  },
                 // {
                 //     data: "studentshiftid"
                 // },
                 //  {
                 //      data: "studentgradeid"
                 //  },
                    {
                        data: "studentstatus"
                    },
                    
                    {
                        data: "id",
                        render: function (data) {
                            return "<button onclick='studentEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:0px;'>Edit</button>" + "<button onclick='studentDelete(" + data + ")' class='btn btn-danger btn-xs' >Delete</button>";
                        }
                    },
                    {
                        data: "id",
                        render: function (data) {
                            return "<button onclick='studentAppropriate(" + data + ")' class='btn-success btn-xs pull-right' style='margin-right:0px;'>Appropriate</button>" + "<button onclick='studentEmergency(" + data + ")' class='btn-success btn-xs pull-right' style='margin-right:0px;'>Emergency</button>" + "<button onclick='studentParrent(" + data + ")' class='btn-success btn-xs pull-right'>Parrent</button>";
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
            $('#studentphoto').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function StudentAction() {
    var action = '';
    action = document.getElementById('btnStudent').innerText;
    if (action == "Add New") {
        document.getElementById('btnStudent').innerText = "Save";
        EnableControl();
        $("#surname").val('');
        $("#firstname").val('');
        $("#fullname").val('');
        $("#fullnamekh").val('');
        
        $("#studentdob").val(moment().format('YYYY-MM-DD'));
        $("#studentstatus").val('REGISTER');
        document.getElementById('studentstatus').disabled = true;
        $("#nationality").val('Khmer');
        $("#studentgender").val('Male');
        $('#studentdob').val(moment().format('YYYY-MM-DD'));
        $('#surname').focus();
        getMaxInvNo();

    } else if (action == "Save") {
        var response = Validate();
        if (response == false) {
            return false;
        }
        var data = new FormData();
        var files = $("#file").get(0).files;
        if (files.length > 0) {
            data.append("studentphoto", files[0]);
        }

        data.append("studentid", $("#studentid").val());
        data.append("studentbranchid", $("#studentbranchid").val());
        data.append("surname", $("#surname").val());
        data.append("firstname", $("#firstname").val());
        data.append("fullname", $("#fullname").val());
        data.append("fullnamekh", $("#fullnamekh").val());
        data.append("studentgender", $("#studentgender").val());
        data.append("studentdob", $("#studentdob").val());
        data.append("studentpob", $("#studentpob").val());
        data.append("nationality", $("#nationality").val());
        data.append("nativelanguage", $("#nativelanguage").val());
        data.append("otherspoken", $("#otherspoken").val());
        data.append("schoolyear", $("#schoolyear").val());
        data.append("studentphoto", $("#studentphoto").val());
        data.append("studentshiftid", $("#studentshiftid").val());
        data.append("studentgradeid", $("#studentgradeid").val());
        data.append("studentstatus", $("#studentstatus").val());
        data.append("createby", $("#createby").val());
        data.append("createdate", $("#createdate").val());

        //console.log(files[0]);

        $.ajax({
            type: "POST",
            url: "/api/Students",
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {

                //console.log(result);

                toastr.success("Student has been created successfully.", "Server Response");
                tableEmployee.ajax.reload();

                $('#id').val(result.id);
                $('#studentModel').modal('hide');

                document.getElementById('btnStudent').innerText = "Add New";
                DisableControl();
                ClearData();

            },
            error: function (error) {
                //console.log(error);
                toastr.error("Student Already Exists!.", "Server Response");
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
            data.append("studentphoto", files[0]);
        }
        data.append("id", $('#id'));
        data.append("studentid", $("#studentid").val());
        data.append("studentbranchid", $("#studentbranchid").val());
        data.append("surname", $("#surname").val());
        data.append("firstname", $("#firstname").val());
        data.append("fullname", $("#fullname").val());
        data.append("fullnamekh", $("#fullnamekh").val());
        data.append("studentgender", $("#studentgender").val());
        data.append("studentdob", $("#studentdob").val());
        data.append("studentpob", $("#studentpob").val());
        data.append("nationality", $("#nationality").val());
        data.append("nativelanguage", $("#nativelanguage").val());
        data.append("otherspoken", $("#otherspoken").val());
        data.append("schoolyear", $("#schoolyear").val());
        //data.append("studentphoto", $("#studentphoto").val());
        data.append("studentshiftid", $("#studentshiftid").val());
        data.append("studentgradeid", $("#studentgradeid").val());
        data.append("studentstatus", $("#studentstatus").val());
        data.append("createby", $("#createby").val());
        data.append("createdate", $("#createdate").val());
        data.append("file_old", $("#file_old").val());

        //console.log(data);

        $.ajax({
            type: "PUT",
            url: "/api/Students/" + $('#id').val(),
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                toastr.success("Student has been updated successfully.", "Server Response");
                tableEmployee.ajax.reload();

                //$('#employeeId').val(result.id);
                $('#studentModel').modal('hide');

                document.getElementById('btnStudent').innerText = "Add New";
                DisableControl();
                ClearData();

            },
            error: function (error) {
                //console.log(error);
                toastr.error("Student Save Not Commplete!.", "Server Response");
            }
        });

    }
}

function studentEdit(id) {
    ClearData();
    EnableControl();
    document.getElementById('btnStudent').innerText = "Update";

    $.ajax({
        url: "/api/Students/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            
            $("#id").val(result.id);
            $("#studentid").val(result.studentid);
            var payno = "SH" + ("000000" + result.studentid).slice(-6);
            $('#stuid').val(payno);
            $("#studentbranchid").val(result.studentbranchid);
            $("#surname").val(result.surname);
            $("#firstname").val(result.firstname);
            $("#fullname").val(result.fullname);
            $("#fullnamekh").val(result.fullnamekh);
            $("#studentgender").val(result.studentgender);
            var dr = moment(result.studentdob).format("YYYY-MM-DD");
            $("#studentdob").val(dr);
            $("#studentpob").val(result.studentpob);
            $("#nationality").val(result.nationality);
            $("#nativelanguage").val(result.nativelanguage);
            $("#otherspoken").val(result.otherspoken);
            $("#schoolyear").val(result.schoolyear);
            //$("#studentphoto").val(result.studentphoto);
            $("#studentshiftid").val(result.studentshiftid);
            $("#studentgradeid").val(result.studentgradeid);
            $("#studentstatus").val(result.studentstatus);
            $("#createby").val(result.createby);
            $("#createdate").val(result.createdate);
            $('#file_old').val(result.studentphoto);

            //console.log(result);

            if (result.studentphoto == "") {
                $('#studentphoto').attr('src', '../Images/no_image.png');
                
            } else {
                //alert(result.studentphoto);
                $('#studentphoto').attr('src', '../Images/' + result.studentphoto);


            }
            $('#studentModel').modal('show');
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });

}

function studentDelete(id) {
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
                    url: "/api/Students/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableEmployee.ajax.reload();
                        toastr.success("Student has been Deleted successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("This Student is already exists in Database", "Service Response");
                    }
                });
            }
        }
    });
}

function DisableControl() {
    document.getElementById('id').disabled = true;
    document.getElementById('studentid').disabled = true;
    document.getElementById('studentbranchid').disabled = true;
    document.getElementById('surname').disabled = true;
    document.getElementById('firstname').disabled = true;
    document.getElementById('fullname').disabled = true;
    document.getElementById('fullnamekh').disabled = true;
    document.getElementById('studentgender').disabled = true;
    document.getElementById('studentdob').disabled = true;
    document.getElementById('studentpob').disabled = true;
    document.getElementById('nationality').disabled = true;
    document.getElementById('nativelanguage').disabled = true;
    document.getElementById('otherspoken').disabled = true;
    document.getElementById('schoolyear').disabled = true;
    document.getElementById('studentphoto').disabled = true;
    document.getElementById('studentshiftid').disabled = true;
    document.getElementById('studentgradeid').disabled = true;
    document.getElementById('studentstatus').disabled = true;
 
    document.getElementById('btnStudent').innerText = "Add New";

}

function EnableControl() {
    document.getElementById('id').disabled = false;
    document.getElementById('studentid').disabled = false;
    document.getElementById('studentbranchid').disabled = false;
    document.getElementById('surname').disabled = false;
    document.getElementById('firstname').disabled = false;
    document.getElementById('fullname').disabled = false;
    document.getElementById('fullnamekh').disabled = false;
    document.getElementById('studentgender').disabled = false;
    document.getElementById('studentdob').disabled = false;
    document.getElementById('studentpob').disabled = false;
    document.getElementById('nationality').disabled = false;
    document.getElementById('nativelanguage').disabled = false;
    document.getElementById('otherspoken').disabled = false;
    document.getElementById('schoolyear').disabled = false;
    document.getElementById('studentphoto').disabled = false;
    document.getElementById('studentshiftid').disabled = false;
    document.getElementById('studentgradeid').disabled = false;
    document.getElementById('studentstatus').disabled = false;

}

function Validate() {
    var isValid = true;
    if ($('#studentid').val().trim() == "") {
        $('#studentid').css('border-color', 'red');
        $('#studentid').focus();
        isValid = false;
    } else if ($('#surname').val().trim() == "") {
        $('#surname').css('border-color', 'red');
        $('#surname').focus();
        isValid = false;
    } else if ($('#firstname').val().trim() == "") {
        $('#firstname').css('border-color', 'red');
        $('#firstname').focus();
        isValid = false;
    } else if ($('#fullname').val().trim() == "") {
        $('#fullname').css('border-color', 'red');
        $('#fullname').focus();
        isValid = false;
    } else if ($('#fullnamekh').val().trim() == "") {
        $('#fullnamekh').css('border-color', 'red');
        $('#fullnamekh').focus();
        isValid = false;
    //} else if ($('#studentshiftid').val().trim() == "") {
    //    $('#studentshiftid').css('border-color', 'red');
    //    $('#studentshiftid').focus();
    //    isValid = false;
    //} else if ($('#studentgradeid').val().trim() == "") {
    //    $('#studentgradeid').css('border-color', 'red');
    //    $('#studentgradeid').focus();
    //    isValid = false;
    //} else if ($('#studentbranchid').val().trim() == "") {
    //    $('#studentbranchid').css('border-color', 'red');
    //    $('#studentbranchid').focus();
    //    isValid = false;
    } else if ($('#studentdob').val().trim() == "") {
        $('#studentdob').css('border-color', 'red');
        $('#studentdob').focus();
        isValid = false;
    } else {

        $('#studentid').css('border-color', '#cccccc');
        $('#surname').css('border-color', '#cccccc');
        $('#firstname').css('border-color', '#cccccc');
        $('#fullname').css('border-color', '#cccccc');
        $('#fullnamekh').css('border-color', '#cccccc');
        $('#studentshiftid').css('border-color', '#cccccc');
        $('#studentgradeid').css('border-color', '#cccccc');
        $('#studentbranchid').css('border-color', '#cccccc');
        isValid = true;
    }
    return isValid;
}


function ClickAddnewStudent() {
    DisableControl();
    document.getElementById('btnStudent').innerText = "Add New";
    $('#studentphoto').attr('src', '../Images/no_image.png');
    
}

function studentEmergency(id) {
    //alert(id);
    GetEmergency(id);   // emergency.js
    $("#emerstudentid").val(id);
    $("#emergencyModel").modal('show');
}

function studentParrent(id) {
    //alert(id);
    GetParrentstudent(id);   // parrentstudent.js
    $("#parrentStuId").val(id);
    $("#parrentstudentModel").modal('show');
}

function studentAppropriate(id) {
    //alert(id);
    GetAppropriate(id);   // appropriate.js
    $("#appstudentid").val(id);
    $("#appropriateModel").modal('show');
}

function getMaxInvNo() {
    $.get("/api/Students/?a=1&b=2", function (data, status) {
        // alert("Data: " + data + "\nStatus: " + status);
        var arrayData = data.split(',');
        //alert(arrayData[0]);
        //alert(arrayData[1]);
        $('#studentid').val(arrayData[0]);
        $('#stuid').val(arrayData[1]);

    });
}