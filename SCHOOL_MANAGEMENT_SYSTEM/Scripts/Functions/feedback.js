
$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });
    
    GetFeedback();
    $('#FeedbackModel').on('show.bs.modal', function () {
    }); 
})


var tableFeedback = [];
function GetFeedback() {
    var feedidtemp = $('#feedbackidtemp').val();
    //alert(departmentId);
    tableFeedback = $('#tableFeedback').DataTable({
        ajax: {
            url: "/api/Feedbacks/" + feedidtemp,
            dataSrc: ""
        },
        columns: [
            //{
            //    data: "id"
            //},
            {
                data: "date",
                render: function (data) {
                    return moment(data).format("DD-MMM-YYYY");
                }
            },
            {
                data: "customer.name",
            },
            {
                data: "comment",
            },
            {
                data: "id",
                render: function (data) {
                    return "<button onclick='FeedbackEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'​>Edit</button>" + "<button onclick='FeedbackDelete(" + data + ")' class='btn btn-danger btn-xs' >Delete</button>";
                }
            },
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}

function readURLFeedback(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#image').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function FeedbackAction() {
    var action = '';
    action = document.getElementById('btnSaveFeedback').innerText;
    //alert(action);

    if (action == "Add New") {
        document.getElementById('btnSaveFeedback').innerText = 'Save';
        $('#comment').val('');
        document.getElementById('comment').disabled = false;
        document.getElementById('customerid').disabled = false;
        document.getElementById('date').disabled = false;
        $('#date').focus().val(moment().format('YYYY-MM-DD'));

    }
    else if (action == "Save") {

        //var res = ValidateCustomer();
        //if (res == false) {
        //    return false;
        //}
        var data = new FormData();

        var files = $('#file').get(0).files;

        if (files.length > 0) {
            data.append("image", files[0]);
        }
        data.append("date", $('#date').val());
        data.append("customerid", $('#customerid').val());
        data.append("comment", $('#comment').val());

        $.ajax({
            url: "/api/Feedbacks",
            data: data,
            type: "POST",
            contentType: false,
            processData: false,
            dataType: "json",
            success: function (result) {
                toastr.success("New Pay has been Created", "Server Respond");
                $('#tableFeedback').DataTable().ajax.reload();
                document.getElementById('btnSaveFeedback').innerText = 'Add New';
                document.getElementById('date').disabled = true;
                document.getElementById('customerid').disabled = true;
                document.getElementById('comment').disabled = true;
                // $('#customerName').val('');
                //$("#customerModal").modal('hide');
                $('#comment').val('');

            },
            error: function (errormesage) {
                $('#date').focus();
                toastr.error("This Name is exist in Database", "Server Respond")
            }

        });

    } else if (action == "Update") {

        var data = new FormData();

        var files = $('#file').get(0).files;

        if (files.length > 0) {
            data.append("image", files[0]);
        }

        data.append("id", $('#fid').val());
        data.append("date", $('#date').val());
        data.append("customerid", $('#customerid').val());
        data.append("comment", $('#comment').val());

        $.ajax({
            url: "/api/Feedbacks/" + $("#fid").val(),
            data: data,
            type: "PUT",
            contentType: false,
            processData: false,
            dataType: "json",
            success: function (result) {
                toastr.success("Customer has been Updated", "Server Respond");
                $('#tableFeedback').DataTable().ajax.reload();
                document.getElementById('btnSaveFeedback').innerText = 'Add New';
                document.getElementById('date').disabled = true;
                document.getElementById('customerid').disabled = true;
                document.getElementById('comment').disabled = true;
                // $('#customerName').val('');
                //$("#customerModal").modal('hide');
                $('#comment').val('');
            },
            error: function (errormesage) {
                toastr.error("Customer hasn't Updated in Database", "Server Respond")
            }
        });
    }
}

function FeedbackEdit(id) {
    ClearData();
    document.getElementById('comment').disabled = false;
    document.getElementById('customerid').disabled = false;
    document.getElementById('date').disabled = false;
    document.getElementById('btnSaveFeedback').innerText = "Update";

    $.ajax({
        url: "/api/Feedbacks/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#fid').val(result.id);
            var date = new Date(result.date);
            var datetime = moment(date).format('YYYY-MM-DD');
            $("#date").val(datetime);
            $('#customerid').val(result.customerid);
            $('#comment').val(result.comment);
            //$('#file_old').val(result.image);

            if (result.image == "") {
                $("#image").attr('src', '../Images/no_image.png');
            } else {
                $("#image").attr('src', '../Images/' + result.image);
            }
            //$('#FeedbackModel').modal('show');
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });

}

function FeedbackDelete(id) {
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
                    url: "/api/Feedbacks/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableFeedback.ajax.reload();
                        toastr.success("Feedback has been Deleted successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("This Feedback is already exists in Database", "Service Response");
                    }
                });
            }
        }
    });
}



function ClickAddnewFeedback() {
    document.getElementById('comment').disabled = true;
    document.getElementById('customerid').disabled = true;
    $('#comment').val('');
    $('#customerid').val('');
    $('#image').attr('src', '../Images/no_image.png');
    document.getElementById('btnSaveFeedback').innerText = "Add New";
}