$(document).ready(function () {
    GetCondition();
    //alert('hi');
});
var tableConditions = [];
function GetCondition() {
    tableCondition = $('#tableCondition').dataTable({
        ajax: {
            url: "/api/Conditions",
            dataSrc: ""
        },
        columns:
            [
                {
                    data: "id",
                },
               
                {
                    data: "conditionnote",
                },
                {
                    data: "createby",
                },
                {
                    data: "date",
                    render: function (data) {
                        return moment(data).format("DD-MMM-YYYY");
                    }
                },
                {
                    data: "employee.name",
                },
                {
                    data: "id",
                    render: function (data) {
                        return "<button OnClick='ConditionEdit (" + data + ")' class='btn btn-primary btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span> Edit</button>" +
                            "<button OnClick='ConditionDelete (" + data + ")' class='btn btn-default btn-xs' ><span class='glyphicon glyphicon-remove'></span> Delete</button>";
                    }
                }
            ],
        destroy: true,

    });
}
function ConditionAction() {
    var action = '';
    action = document.getElementById('btnSaveCondition').innerText;
    //DisableControl();
    if (action == "Add New") {
        document.getElementById('btnSaveCondition').innerText = 'Save';
        EnableControl();

        $('#date').focus().val(moment().format('YYYY-MM-DD'));
    }

    if (action === "Save") {
        //Validate();
        var data = {
            date: $('#date').val(),
            conditionnote: $('#conditionnote').val(),
            employeeid: $('#employeeid').val(),
        };
        $.ajax({
            url: "/api/Conditions",
            data: JSON.stringify(data),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                toastr.success("Save to database successfully.", "Server Response");
                $('#tableCondition').DataTable().ajax.reload();

                $('#ConditionModel').modal('hide');
                //document.getElementById('btnSaveCondition').innerHTML = "<span class='glyphicon glyphicon-plus-sign'></span> <span class='kh'>បង្កើតថ្មី</span> / Add New";

            },
            error: function (errormessage) {
                toastr.error("This Condition is already exists.", "Server Response");
                //document.getElementById('btnSaveCondition').innerHTML = "<span class='glyphicon glyphicon-plus-sign'></span> <span class='kh'>បង្កើតថ្មី</span> / Add New";

            }
        });
    }
    else if (action === "Update") {
        //alert('hi');
        var data = {
            id: $('#id').val(),
            date: $('#date').val(),
            conditionnote: $('#conditionnote').val(),
            employeeid: $('#employeeid').val(),
        };
        $.ajax({
            url: "/api/Conditions/" + data.id,
            data: JSON.stringify(data),
            type: "PUT",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                toastr.success("Updated successfully.", "Server Response");
                $('#tableCondition').DataTable().ajax.reload();

                $('#ConditionModel').modal('hide');
                //tableConditions.ajax.reload();
                //$('#registerModal').modal('hide');

            },
            error: function (errormessage) {
                toastr.error("This Condition can't Update.", "Server Response");

            }
        });
    }
}


function ConditionEdit(id) {
    ClearControl();
    EnableControl();

    //$('#status').val('');
    action = document.getElementById('btnSaveCondition').innerText = "Update";

    $.ajax({
        url: "/api/Conditions/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            $('#id').val(result.id);

            var date = new Date(result.date);
            var datetime = moment(date).format('YYYY-MM-DD');
            $('#date').val(datetime);
            $('#conditionnote').val(result.conditionnote);
            $('#employeeid').val(result.employeeid);

            $("#ConditionModel").modal('show');
        },
        error: function (errormessage) {
            toastr.error("Something unexpected happen.", "Server Response");
        }
    });
    return false;
}


function ConditionDelete(id) {
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
                    url: "/api/Conditions/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        $('#tableCondition').DataTable().ajax.reload();

                        toastr.success("Condition Deleted successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("Condition Can't be Deleted", "Service Response");
                    }
                });
            }
        }
    });
}



function DisableControl() {
    document.getElementById('date').disabled = true;
    document.getElementById('conditionnote').disabled = true;
    document.getElementById('employeeid').disabled = true;
}

function EnableControl() {

    document.getElementById('date').disabled = false;
    document.getElementById('conditionnote').disabled = false;
    document.getElementById('employeeid').disabled = false;
}

function ClearControl() {
    $('#conditionnote').val('');
    $('#employeeid').val('');

}

function ClickAddnewCondition() {
    document.getElementById('btnSaveCondition').innerText = "Add New";
    DisableControl();
    ClearControl();
}

function Validate() {
    var isValid = true;
    var formAddEdit = $("#formTrainingProgramAdd");
    if ($('#ConditionTittle').val().trim() === "") {
        $('#ConditionTittle').css('border-color', 'red');
        $('#ConditionTittle').focus();
        toastr.info("Please enter Condition title", "Required");
    }
    else {
        $('#ConditionTittle').css('border-color', '#cccccc');
        if ($('#createBy').val().trim() === "") {
            $('#createBy').css('border-color', 'red');
            $('#createBy').focus();
            toastr.info("Please enter your phone", "Required");
        }
        else {
            $('#createBy').css('border-color', '#cccccc');
        }
    }
    return isValid;
}