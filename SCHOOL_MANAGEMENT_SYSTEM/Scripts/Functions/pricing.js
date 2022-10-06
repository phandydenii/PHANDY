$(document).ready(function () {
    GetPricing();
    //alert('hi');
});
var tablePricings = [];
function GetPricing() {
    tablePricing = $('#tablePricing').dataTable({
        ajax: {
            url: "/api/Pricings",
            dataSrc: ""
        },
        columns:
            [
                {
                    data: "id",
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
                    data: "descrition",
                },
                {
                    data: "pricing",
                },
                
                {
                    data: "createby",
                },               
                
                {
                    data: "id",
                    render: function (data) {
                        return "<button OnClick='PricingEdit (" + data + ")' class='btn btn-primary btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span> Edit</button>" +
                            "<button OnClick='PricingDelete (" + data + ")' class='btn btn-default btn-xs' ><span class='glyphicon glyphicon-remove'></span> Delete</button>";
                    }
                }
            ],
        destroy: true,

    });
}
function PricingAction() {
    var action = '';
    action = document.getElementById('btnSavePricing').innerText;
    //DisableControl();
    if (action == "Add New") {
        document.getElementById('btnSavePricing').innerText = 'Save';
        EnableControl();

        $('#date').focus().val(moment().format('YYYY-MM-DD'));
    }

    if (action === "Save") {
        //Validate();
        var data = {
            date: $('#date').val(),
            descrition: $('#description').val(),
            employeeid: $('#employeeid').val(),
            pricing: $('#pricing').val(),
        };
        $.ajax({
            url: "/api/Pricings",
            data: JSON.stringify(data),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                toastr.success("Save to database successfully.", "Server Response");
                $('#tablePricing').DataTable().ajax.reload();

                $('#PricingModel').modal('hide');
                //document.getElementById('btnSavePricing').innerHTML = "<span class='glyphicon glyphicon-plus-sign'></span> <span class='kh'>បង្កើតថ្មី</span> / Add New";

            },
            error: function (errormessage) {
                toastr.error("This Pricing is already exists.", "Server Response");
                //document.getElementById('btnSavePricing').innerHTML = "<span class='glyphicon glyphicon-plus-sign'></span> <span class='kh'>បង្កើតថ្មី</span> / Add New";

            }
        });
    }
    else if (action === "Update") {
        //alert('hi');
        var data = {
            id: $('#id').val(),
            date: $('#date').val(),
            descrition: $('#description').val(),
            employeeid: $('#employeeid').val(),
            pricing: $('#pricing').val(),
        };
        $.ajax({
            url: "/api/Pricings/" + data.id,
            data: JSON.stringify(data),
            type: "PUT",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                toastr.success("Updated successfully.", "Server Response");
                $('#tablePricing').DataTable().ajax.reload();

                $('#PricingModel').modal('hide');
                //tablePricings.ajax.reload();
                //$('#registerModal').modal('hide');

            },
            error: function (errormessage) {
                toastr.error("This Pricing can't Update.", "Server Response");

            }
        });
    }
}


function PricingEdit(id) {
    ClearControl();
    EnableControl();

    //$('#status').val('');
    action = document.getElementById('btnSavePricing').innerText = "Update";

    $.ajax({
        url: "/api/Pricings/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            $('#id').val(result.id);

            var date = new Date(result.date);
            var datetime = moment(date).format('YYYY-MM-DD');
            $('#date').val(datetime);
            $('#description').val(result.descrition);
            $('#employeeid').val(result.employeeid);
            $('#pricing').val(result.pricing);

            $("#PricingModel").modal('show');
        },
        error: function (errormessage) {
            toastr.error("Something unexpected happen.", "Server Response");
        }
    });
    return false;
}


function PricingDelete(id) {
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
                    url: "/api/Pricings/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        $('#tablePricing').DataTable().ajax.reload();

                        toastr.success("Pricing Deleted successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("Pricing Can't be Deleted", "Service Response");
                    }
                });
            }
        }
    });
}


function DisableControl() {
    document.getElementById('date').disabled = true;
    document.getElementById('description').disabled = true;
    document.getElementById('employeeid').disabled = true;
    document.getElementById('pricing').disabled = true;
}

function EnableControl() {

    document.getElementById('date').disabled = false;
    document.getElementById('description').disabled = false;
    document.getElementById('employeeid').disabled = false;
    document.getElementById('pricing').disabled = false;
}

function ClearControl() {
    $('#description').val('');
    $('#employeeid').val('');
    $('#pricing').val('');

}

function ClickAddnewPricing() {
    document.getElementById('btnSavePricing').innerText = "Add New";
    DisableControl();
    ClearControl();
}

function Validate() {
    var isValid = true;
    var formAddEdit = $("#formTrainingProgramAdd");
    if ($('#PricingTittle').val().trim() === "") {
        $('#PricingTittle').css('border-color', 'red');
        $('#PricingTittle').focus();
        toastr.info("Please enter Pricing title", "Required");
    }
    else {
        $('#PricingTittle').css('border-color', '#cccccc');
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