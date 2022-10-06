$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });

    $('#appropriateModel').on('show.bs.modal', function () {

        //GetParrent(id);
    })
})

var tableParrent = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';
var chkEng = 0;
var chkCh = 0;
var uStudy = 0;
var tLat = 0;
var iNess = 0;
var aGies = 0;
var fear = 0;
var pSport = 0;
var aPhoto = 0;

function GetAppropriate(id) {
    //alert("Hello");
    tableParrent = $('#appropriateTable').DataTable({
        ajax: {
            url: "/api/AppropriateByStudent/" + id,
            dataSrc: ""
        },
        columns: [

            //{
            //    data: "appid"
            //},
            //{
            //    data: "appstudentid"
            //},
            {
                data: "livewith"
            },
            {
                data: "speakhome"
            },
            //{
            //    data: "speakenglish"
            //},
            //{
            //    data: "speakchinese"
            //},
            //{
            //    data: "usetostudy"
            //},
            {
                data: "usetostudynote"
            },
            //{
            //    data: "toilattrained"
            //},
            //{
            //    data: "illness"
            //},
            {
                data: "illnessnote"
            },
            //{
            //    data: "allergies"
            //},
            {
                data: "allergiesnote"
            },
            //{
            //    data: "fears"
            //},
            {
                data: "fearsnote"
            },
            {
                data: "sufferinfor"
            },
            {
                data: "othernote"
            },
            //{
            //    data: "playsport"
            //},
            {
                data: "playsportnote"
            },
            //{
            //    data: "allowphoto"
            //},
            {
                data: "appid",
                render: function (data) {
                    return "<button onclick='appropriateEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>" + "<button onclick='appropriateDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });

    DisableControl();
}

function appropriateAction() {
    var action = '';
    action = document.getElementById('btnAppropriate').innerText;
    if ($("#speakenglish").is(':checked')) {
         chkEng = 1;
    } else {
         chkEng = 0;
    }

    if ($("#speakchinese").is(':checked')) {
        chkCh = 1;
    } else {
        chkCh = 0;
    }

    if ($("#usetostudy").is(':checked')) {
        uStudy = 1;
    } else {
        uStudy = 0;
    }

    if ($("#toilattrained").is(':checked')) {
        tLat = 1;
    } else {
        tLat = 0;
    }

    if ($("#illness").is(':checked')) {
        iNess = 1;
    } else {
        iNess = 0;
    }

    if ($("#allergies").is(':checked')) {
        aGies = 1;
    } else {
        aGies = 0;
    }

    if ($("#fears").is(':checked')) {
        fear = 1;
    } else {
        fear = 0;
    }

    if ($("#playsport").is(':checked')) {
        pSport = 1;
    } else {
        pSport = 0;
    }

    if ($("#allowphoto").is(':checked')) {
        aPhoto = 1;
    } else {
        aPhoto = 0;
    }



    if (action == "Add New") {
        document.getElementById('btnAppropriate').innerText = "Save";
        EnableControl();
        $('#livewith').focus();


    } else if (action == "Save") {
        if ($('#livewith').val().trim() == "") {
            $('#livewith').css('border-color', 'red');
            $('#livewith').focus();
            toastr.info('Please enter livewith Name.', "Server Response")
        } else {
            $('#livewith').css('border-color', '#cccccc');

            var dataSave = {
                appstudentid: $('#appstudentid').val(),
                livewith: $('#livewith').val(),
                speakhome: $('#speakhome').val(),
                speakenglish: chkEng,
                speakchinese: chkCh,
                usetostudy: uStudy,
                usetostudynote: $('#usetostudynote').val(),
                toilattrained: tLat,
                illness: iNess,
                illnessnote: $('#illnessnote').val(),
                allergies: aGies,
                allergiesnote: $('#allergiesnote').val(),
                fears: fear,
                fearsnote: $('#fearsnote').val(),
                sufferinfor: $('#sufferinfor').val(),
                othernote: $('#othernote').val(),
                playsport: pSport,
                playsportnote: $('#playsportnote').val(),
                allowphoto: aPhoto,
                //createdate: $('#createDate').val(),
                //createby: $('#createBy').val()
            };



            $.ajax({
                url: "/api/Appropriates",
                data: JSON.stringify(dataSave),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Appropriate has been created successfully.", "Server Response");
                    tableParrent.ajax.reload();
                    DisableControl();
                    ClearData();

                    //GetParrent(id);
                },
                error: function (errormessage) {
                    toastr.error("This Appropriate is already exists in Database", "Service Response");
                }
            })
        }
    } else if (action == "Update") {
        if ($('#livewith').val().trim() == "") {
            $('#livewith').css('border-color', 'red');
            $('#livewith').focus();
            toastr.info('Please enter Livewith Name.', "Server Response")
        } else {
            $('#livewith').css('border-color', '#cccccc');

            var data = {
                appid: $('#appid').val(),
                appstudentid: $('#appstudentid').val(),
                livewith: $('#livewith').val(),
                speakhome: $('#speakhome').val(),
                speakenglish: chkEng,
                speakchinese: chkCh,
                usetostudy: uStudy,
                usetostudynote: $('#usetostudynote').val(),
                toilattrained: tLat,
                illness: iNess,
                illnessnote: $('#illnessnote').val(),
                allergies: aGies,
                allergiesnote: $('#allergiesnote').val(),
                fears: fear,
                fearsnote: $('#fearsnote').val(),
                sufferinfor: $('#sufferinfor').val(),
                othernote: $('#othernote').val(),
                playsport: pSport,
                playsportnote: $('#playsportnote').val(),
                allowphoto: aPhoto,

            };

            //console.log(data);

            $.ajax({
                url: "/api/Appropriates/" + data.appid,
                data: JSON.stringify(data),
                type: "PUT",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Appropriate has been update successfully.", "Server Response");
                    tableParrent.ajax.reload();
                    DisableControl();
                    ClearData();

                },
                error: function (errormessage) {
                    toastr.error("This Appropriate is already exists in Database", "Service Response");
                }
            })
        }
    }

}

function appropriateEdit(id) {
    //alert(id);
    
    $.ajax({
        url: "/api/Appropriates/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#appid').val(result.appid),
            $('#appstudentid').val(result.appstudentid),
            $('#livewith').val(result.livewith),
            $('#speakhome').val(result.speakhome),
            $("#speakenglish").prop('checked', result.speakenglish > 0),
            $("#speakchinese").prop('checked', result.speakchinese > 0),
            $("#usetostudy").prop('checked', result.usetostudy > 0),
            $('#usetostudynote').val(result.usetostudynote),
            $("#toilattrained").prop('checked', result.toilattrained > 0),
            $("#illness").prop('checked', result.illness > 0),
            $('#illnessnote').val(result.illnessnote),
            $("#allergies").prop('checked', result.allergies > 0),
            $('#allergiesnote').val(result.allergiesnote),
            $("#fears").prop('checked', result.fears > 0),
            $('#fearsnote').val(result.fearsnote),
            $('#sufferinfor').val(result.sufferinfor),
            $('#othernote').val(result.othernote),
            $("#playsport").prop('checked', result.playsport > 0),
            $('#playsportnote').val(result.playsportnote),
            $("#allowphoto").prop('checked', result.allowphoto > 0),
            
            document.getElementById('btnAppropriate').innerText = "Update";
            //alert(result.Id);
            EnableControl();
            $('#livewith').focus();
        },
        error: function (errormessage) {
            toastr.error("This no Appropriate in Database", "Service Response");
        }
    });

}

function appropriateDelete(id) {
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
                    url: "/api/Appropriates/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableParrent.ajax.reload();
                        toastr.success("Appropriate has been Deleted successfully!", "Service Response");

                    },
                    error: function (errormessage) {
                        toastr.error("This Appropriate Cannot Delete from Database!", "Service Response");

                    }
                });
            }
        }
    });

}

function DisableControl() {
    document.getElementById('livewith').disabled = true;
    document.getElementById('speakhome').disabled = true;
    document.getElementById('speakenglish').disabled = true;
    document.getElementById('speakchinese').disabled = true;
    document.getElementById('usetostudy').disabled = true;
    document.getElementById('usetostudynote').disabled = true;
    document.getElementById('toilattrained').disabled = true;
    document.getElementById('illness').disabled = true;
    document.getElementById('illnessnote').disabled = true;
    document.getElementById('allergies').disabled = true;
    document.getElementById('allergiesnote').disabled = true;
    document.getElementById('fears').disabled = true;
    document.getElementById('fearsnote').disabled = true;
    document.getElementById('sufferinfor').disabled = true;
    document.getElementById('othernote').disabled = true;
    document.getElementById('playsport').disabled = true;
    document.getElementById('playsportnote').disabled = true;
    document.getElementById('allowphoto').disabled = true;
    document.getElementById('btnAppropriate').innerText = "Add New";
}

function EnableControl() {
    document.getElementById('livewith').disabled = false;
    document.getElementById('speakhome').disabled = false;
    document.getElementById('speakenglish').disabled = false;
    document.getElementById('speakchinese').disabled = false;
    document.getElementById('usetostudy').disabled = false;
    document.getElementById('usetostudynote').disabled = false;
    document.getElementById('toilattrained').disabled = false;
    document.getElementById('illness').disabled = false;
    document.getElementById('illnessnote').disabled = false;
    document.getElementById('allergies').disabled = false;
    document.getElementById('allergiesnote').disabled = false;
    document.getElementById('fears').disabled = false;
    document.getElementById('fearsnote').disabled = false;
    document.getElementById('sufferinfor').disabled = false;
    document.getElementById('othernote').disabled = false;
    document.getElementById('playsport').disabled = false;
    document.getElementById('playsportnote').disabled = false;
    document.getElementById('allowphoto').disabled = false;
}

function ClearData() {
    $('#appid').val('');
    $('#appstudentid').val('');
    $('#livewith').val(''),
    $('#speakhome').val(''),
    $('#speakenglish').val(''),
    $('#speakchinese').val(''),
    $('#usetostudy').val(''),
    $('#usetostudynote').val(''),
    $('#toilattrained').val(''),
    $('#illness').val(''),
    $('#illnessnote').val(''),
    $('#allergies').val(''),
    $('#allergiesnote').val(''),
    $('#fears').val(''),
    $('#fearsnote').val(''),
    $('#sufferinfor').val(''),
    $('#othernote').val(''),
    $('#playsport').val(''),
    $('#playsportnote').val(''),
    $('#allowphoto').val('')
}