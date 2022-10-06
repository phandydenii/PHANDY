$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });
    GetPayment();
    //document.getElementById('tablePaymentDetail').hidden = true;
    //$('#paymentModel').on('show.bs.modal', function () {
    //        
    //})
})

var tableParrent = [];
var products = [];
var cart = [];
var items = [];

toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';
var chkFood = 0;
var maxid = 0;

function GetPayment() {
    
    tableParrent = $('#tablePayment').DataTable({
        ajax: {
            url: "/api/Payments",
            dataSrc: ""
        },
        columns: [
            //{
            //    data: "id"
            //},
            {
                data: "paymentno", render: function (data) {
                    
                    return "EIS"+("000000" + data).slice(-6);
                }
            },
            {
                data: "paymentdate",
                render: function (data) {
                    return moment(new Date(data)).format('DD-MMM-YYYY');
                }
            },
            //{
            //    data: "studentid"
            //},
            {
                data: "fullname",
            },
            {
                data: "fullnamekh",
            },
            {
                data: "shiftname",
            },
            {
                data: "gradename",
            },
            {
                data: "enrolldate",
                render: function (data) {
                    return moment(new Date(data)).format('DD-MMM-YYYY');
                }
            },
            //{
            //    data: "adminfee"
            //},
            //{
            //    data: "food"
            //},
            //{
            //    data: "duration"
            //},
            {
                data: "expireddate",
                render: function (data) {
                    return moment(new Date(data)).format('DD-MMM-YYYY');
                }
            },
            //{
            //    data: "dayextend"
            //},
            //{
            //    data: "paymentstatus"
            //},
            //{
            //    data: "overdate"
            //},
            //{
            //    data: "note"
            //},
            //{
            //    data: "userid"
            //},
            //{
            //    data: "shiftid"
            //},
            //{
            //    data: "gradeid"
            //},
            //{
            //    data: "createby"
            //},
            //{
            //    data: "MaxID"
            //},
            {
                data: "id",
                render: function (data) {
                    return "<button onclick='openInvoice(" + data + ")'  class='btn btn-info btn-xs pull-center' style='margin-right: 5px;'>View Invoice</button>" + "<button onclick='PaymentEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>" + "<button onclick='PaymentDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
    
    
}

function PaymentAction() {
    var action = '';
    if ($("#food").is(':checked')) {
        chkFood = 1;
    } else {
        chkFood = 0;
    }

    action = document.getElementById('btnPayment').innerText;
    if (action == "Add New") {
        document.getElementById('btnPayment').innerText = "Save";
        document.getElementById('paymentnoformat').disabled = false;
        document.getElementById('paymentdate').disabled = false;
        document.getElementById('studentidpayment').disabled = false;
        document.getElementById('enrolldate').disabled = false;
        document.getElementById('adminfee').disabled = false;
        document.getElementById('food').disabled = false;
        document.getElementById('duration').disabled = false;
        document.getElementById('expireddate').disabled = false;
        document.getElementById('dayextend').disabled = false;
        document.getElementById('paymentstatus').disabled = false;
        document.getElementById('overdate').disabled = false;
        document.getElementById('note').disabled = false;
        document.getElementById('shiftid').disabled = false;
        document.getElementById('gradeid').disabled = false;

        $('#paymentdate').val(moment().format('YYYY-MM-DD'))
        $('#enrolldate').val(moment().format('YYYY-MM-DD'))
        $('#expireddate').val(moment().format('YYYY-MM-DD'))
        getMaxInvNo();
        


    } else if (action == "Save") {
        var table = document.getElementById('tblCart');
        var rowCount = table.rows.length;
        //alert(rowCount);
        if (rowCount<=1) {
            toastr.info('Please Select Course!.', "Server Response")
        } else {
            //save to paymentdetail
            
            for (var i = 0; i < cart.length; i++) {

                var AddItem = {
                    paymentid: cart[i].paymentid,
                    courseid: cart[i].product.course_id,
                    qty: cart[i].product.qty_fee,
                    turtionfee: cart[i].product.turtion_fee,
                    discount: cart[i].product.discount_fee,
                    total: cart[i].product.total_fee,

                };
                items.push(
                    {
                        paymentid: cart[i].paymentid,
                        courseid: cart[i].product.course_id,
                        qty: cart[i].product.qty_fee,
                        turtionfee: cart[i].product.turtion_fee,
                        discount: cart[i].product.discount_fee,
                        total: cart[i].product.total_fee,
                    });
            }
            //save to payment
            var dataOrder = {
                    paymentno: $('#paymentno').val(),
                    paymentdate: $('#paymentdate').val(),
                    studentid: $('#studentidpayment').val(),
                    enrolldate: $('#enrolldate').val(),
                    adminfee: $('#adminfee').val(),
                    food: chkFood,
                    duration: $('#duration').val(),
                    expireddate: $('#expireddate').val(),
                    dayextend: $('#dayextend').val(),
                    paymentstatus: $('#paymentstatus').val(),
                    overdate: $('#overdate').val(),
                    note: $('#note').val(),
                    userid: $('#userid').val(),
                    shiftid: $('#shiftid').val(),
                    gradeid: $('#gradeid').val(),
                    deposit: $('#deposit').val(),
                    depositr: $('#depositr').val(),

                items: items
            }

            //console.log(dataOrder);
            $.ajax({
                url: "/api/Payments",
                type: "POST",
                data: JSON.stringify(dataOrder),
                contentType: "application/json",
                success: function (data) {
                    toastr.success("Record Save Successfully");
                    //Delete Row from paymentdetail table
                    var tableHeaderRowCount = 1;
                    var table = document.getElementById('tblCart');
                    var rowCount = table.rows.length;
                    for (var i = tableHeaderRowCount; i < rowCount; i++) {
                        table.deleteRow(tableHeaderRowCount);
                    }
                   

                    $('#paymentModel').modal('hide');
                    var table = $('#tablePayment').DataTable();
                    table.ajax.reload();
                  
                },
                error: function (err) {
                    console.log("False")
                    toastr.error("Record Save Error");
                }
            });
        }
    } else if (action == "Update") {
        var table = document.getElementById('tblCart');
        var rowCount = table.rows.length;
        //alert(rowCount);
        if (rowCount <= 1) {
            toastr.info('Please Select Course!.', "Server Response")
        } else {
            //update to paymentdetail

            for (var i = 0; i < cart.length; i++) {

                var AddItem = {
                    id:cart[i].id,
                    paymentid: cart[i].payment_id,
                    courseid: cart[i].product.course_id,
                    qty: cart[i].product.qty_fee,
                    turtionfee: cart[i].product.turtion_fee,
                    discount: cart[i].product.discount_fee,
                    total: cart[i].product.total_fee,

                };
                items.push(
                    {
                        id:cart[i].id,
                        paymentid: cart[i].payment_id,
                        courseid: cart[i].product.course_id,
                        qty: cart[i].product.qty_fee,
                        turtionfee: cart[i].product.turtion_fee,
                        discount: cart[i].product.discount_fee,
                        total: cart[i].product.total_fee,
                    });
            }
            //update to payment
            var dataOrder = {
                id: $('#id').val(),
                paymentno: $('#paymentno').val(),
                paymentdate: $('#paymentdate').val(),
                studentid: $('#studentidpayment').val(),
                enrolldate: $('#enrolldate').val(),
                adminfee: $('#adminfee').val(),
                food: chkFood,
                duration: $('#duration').val(),
                expireddate: $('#expireddate').val(),
                dayextend: $('#dayextend').val(),
                paymentstatus: $('#paymentstatus').val(),
                overdate: $('#overdate').val(),
                note: $('#note').val(),
                userid: $('#userid').val(),
                shiftid: $('#shiftid').val(),
                gradeid: $('#gradeid').val(),
                deposit: $('#deposit').val(),
                depositr: $('#depositr').val(),

                items: items
            }

            //console.log(dataOrder);
            $.ajax({
                url: "/api/Payments/" + dataOrder.id,
                type: "PUT",
                data: JSON.stringify(dataOrder),
                contentType: "application/json",
                success: function (data) {
                    toastr.success("Record Update Successfully");
                    //Delete Row from paymentdetail table
                    var tableHeaderRowCount = 1;
                    var table = document.getElementById('tblCart');
                    var rowCount = table.rows.length;
                    for (var i = tableHeaderRowCount; i < rowCount; i++) {
                        table.deleteRow(tableHeaderRowCount);
                    }


                    $('#paymentModel').modal('hide');
                    var table = $('#tablePayment').DataTable();
                    table.ajax.reload();

                },
                error: function (err) {
                    console.log("False")
                    toastr.info("Record No any update!");
                }
            });
        }
    }

}

function PaymentEdit(id) {
    products = [];
    cart = [];
    items = [];
    $('#demo2').html("");
    $('#paymentModel').modal('show');
    document.getElementById('paymentnoformat').disabled = false;
    document.getElementById('paymentdate').disabled = false;
    document.getElementById('studentidpayment').disabled = false;
    document.getElementById('enrolldate').disabled = false;
    document.getElementById('adminfee').disabled = false;
    document.getElementById('food').disabled = false;
    document.getElementById('duration').disabled = false;
    document.getElementById('expireddate').disabled = false;
    document.getElementById('dayextend').disabled = false;
    document.getElementById('paymentstatus').disabled = false;
    document.getElementById('overdate').disabled = false;
    document.getElementById('note').disabled = false;
    document.getElementById('shiftid').disabled = false;
    document.getElementById('gradeid').disabled = false;
    $.ajax({
        url: "/api/Payments/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            //alert(result.id);
            $('#id').val(result.id);
            $('#paymentno').val(result.paymentno);
            var payno = "EIS" + ("000000" + result.paymentno).slice(-6);
            //alert(payno);
            $('#paymentnoformat').val(payno);
            var pd = moment(result.paymentdate).format("YYYY-MM-DD");
            $('#paymentdate').val(pd);
            //$('#studentidpayment').val(result.studentid);
            $("#studentidpayment").val(result.studentid).change();
            
            var ed = moment(result.enrolldate).format("YYYY-MM-DD");
            $('#enrolldate').val(ed);
            $('#adminfee').val(result.adminfee);
            $("#food").prop('checked', result.food > 0),
            $('#duration').val(result.duration);
            var epd = moment(result.expireddate).format("YYYY-MM-DD");
            $('#expireddate').val(epd);
            $('#dayextend').val(result.dayextend);
            $('#paymentstatus').val(result.paymentstatus);
            $('#overdate').val(result.overdate);
            $('#note').val(result.note);
            $('#userid').val(result.userid);
            $('#shiftid').val(result.shiftid);
            $('#gradeid').val(result.gradeid);
            $('#deposit').val(result.deposit);
            $('#depositr').val(result.depositr);
            $('#discount').val(0);
            //alert(result.id);
            LoadPaymentDetail(result.id);
            document.getElementById('btnPayment').innerText = "Update";
            EnableControl();
            //$('#paymentno').focus();
            MaxExchange();
        },
        error: function (errormessage) {
            toastr.error("Load Record Error", "Service Response");
        }
    });

}

function PaymentDelete(id) {
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
                    url: "/api/PaymentDeletes/" + id,
                    type: "PUT",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableParrent.ajax.reload();
                        toastr.success("Payment has been Deleted successfully!", "Service Response");

                    },
                    error: function (errormessage) {
                        toastr.error("This Payment Cannot Delete from Database!", "Service Response");

                    }
                });
            }
        }
    });

}

function ClickAddnewPayment() {
    //////clear
    $('#demo2').html("");
    ////////end lcear
    $('#paymentModel').modal('show');
    document.getElementById('paymentnoformat').disabled = true;
    document.getElementById('paymentdate').disabled = true;
    document.getElementById('studentidpayment').disabled = true;
    document.getElementById('enrolldate').disabled = true;
    document.getElementById('adminfee').disabled = true;
    document.getElementById('food').disabled = true;
    document.getElementById('duration').disabled = true;
    document.getElementById('expireddate').disabled = true;
    document.getElementById('dayextend').disabled = true;
    document.getElementById('paymentstatus').disabled = true;
    document.getElementById('overdate').disabled = true;
    document.getElementById('note').disabled = true;
    document.getElementById('shiftid').disabled = true;
    document.getElementById('gradeid').disabled = true;
    document.getElementById('btnPayment').innerText = "Add New";
    DisableControl();
    //ClearData();
    $('#paymentnoformat').val('');
    $('#paymentdate').val('');
    $('#enrolldate').val('');
    $('#adminfee').val('0');
    $('#food').val('');
    $('#duration').val('0');
    $('#expireddate').val('');
    $('#dayextend').val('0');
    $('#overdate').val('0');
    $('#note').val('');
    MaxExchange();


    products = [];
    cart = [];
    items = [];

}

function MaxExchange() {
    //Get Last Exchange
    $.ajax({
        url: "/api/ExchangeRates/?a=1&b=2",
        method: "GET",
        success: function (data) {
            $("#txtrate").val(data);
            $("#lblrate").text(data);
            //console.log(data);
        },
        error: function (err) {
            toastr.error("no data record");
        }
    });

}

function openModelCourse() {
    tableproductList = $('#tableCourseList').DataTable({
        ajax: {
            url: "/api/Courses",
            dataSrc: ""
        },
        columns: [
            {
                data: "id"
            },
            {
                data: "coursecode"
            },
            {
                data: "coursename"
            },
            {
                data: "coursenamekh"
            },
            {
                data: "tutionfee"
            },
            {
                data: "id",
                render: function (data) {
                    return "<button onclick='addOrderProduct(" + data + ")'  data-toggle='modal' data-target='#productUnitModal' class='btn btn-success btn-xs' style='margin-right: 5px;'>Add</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
    
}

function addOrderProduct(id) {

    $.ajax({
        url: "/api/Courses/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            addProduct(0,result.id, 0, result.coursename, result.coursenamekh,1, result.tutionfee, 0, result.tutionfee)
            //alert(result.id);
        },
        statusCode: {
            400: function () {
                $('#coursenamekh').focus();
                toastr.error("This Course exists in database.");
            }

        }
    });
    //document.getElementById('tablePaymentDetail').hidden = true;
    
}
//       addProduct(obj.id,obj.courseid, obj.paymentid, obj.coursename, obj.coursenamekh, obj.qty, obj.turtionfee, obj.discount, obj.total)
//
function DiscountPress(id) {
    var dis= $("#discount_" + id).val();
    
    for (var i = 0; i < cart.length; i++) {
        if (cart[i].product.id == id) {
            cart[i].product.qty_fee;
            //alert(dis);
            cart[i].product.discount_fee = dis;
            cart[i].product.total_fee = (cart[i].product.turtion_fee * cart[i].product.qty_fee) - (cart[i].product.turtion_fee * cart[i].product.qty_fee * dis / 100);
        }
        
    }
    
    renderCartTable();
}
function addProduct(id, courseid, paymentid, coursename, coursenamekh, qty, turtionfee, discount, total) {

    var newProduct = {
        id: null,
        course_id: null,
        payment_id:null,
        course_name: null,
        course_namekh: null,
        qty_fee:1,
        turtion_fee: 0,
        discount_fee: 0.00,
        total_fee: 0.00

    };
    newProduct.id = id;
    newProduct.course_id = courseid;
    newProduct.payment_id = paymentid;
    newProduct.course_name = coursename;
    newProduct.course_namekh = coursenamekh;
    newProduct.qty_fee = qty;
    newProduct.turtion_fee = turtionfee;
    newProduct.discount_fee = discount;
    newProduct.total_fee = total;
    products.push(newProduct);
    //alert(newProduct);
    addCart(courseid);

}

function addCart(courseid) {
    for (var i = 0; i < products.length; i++) {
        if (products[i].course_id == courseid) {
            var cartItem = null;
            for (var k = 0; k < cart.length; k++) {
                if (cart[k].product.course_id == courseid) {
                    cartItem = cart[k];
                    //cart[k].qty_fee++;

                    //toastr.error("Products already add!");
                    break;
                    
                }
            }
            if (cartItem == null) {
                var cartItem = {
                    product: products[i],
                    id: products[i].id,
                    course_id: products[i].course_id,
                    payment_id: products[i].payment_id,
                    course_name: products[i].course_name,
                    course_namekh: products[i].course_namekh,
                    qty_fee: products[i].qty_fee,
                    turtion_fee: products[i].turtion_fee,
                    discount_fee: products[i].discount_fee,
                    total_fee: products[i].total_fee
                };

                cart.push(cartItem);
            }
        }
    }

    renderCartTable();
   
}

function renderCartTable() {
    var html = '';
    var ele = document.getElementById("demo2");
    ele.innerHTML = '';

    html += "<table id='tblCart' border='1|1' class='table-hover table-bordered table-condensed table-striped' width='100%'>";
    html += "<thead>";
    html += "<tr>";
    html += "<th>#</th>";
    //html += "<th>id</th>";//id for paymentdetailid
    //html += "<th>CourseID</th>";
    html += "<th>Coursename</th>";
    html += "<th>Coursenamekh</th>";
    html += "<th>Quantity</th>";
    html += "<th>TurtionFee</th>";
    html += "<th>Discount</th>";
    html += "<th>Total</th>";
    //html += "<th>Status</th>";
    html += "<th>Action</th>";
    html += "</tr>"
    html += "</thead>"
    html += "<tboody id='removeTr'>";
    var GrandTotal = 0;
    var Discount = 0;
    var GrandTotalR = 0;
    for (var i = 0; i < cart.length; i++) {
        var no = i + 1;
        html += "<tr>";
        html += "<td>" + no + "</td>";
        //html += "<td>" + cart[i].product.id + "</td>";
        //html += "<td>" + cart[i].product.course_id + "</td>";
        html += "<td>" + cart[i].product.course_name + "</td>";
        html += "<td>" + cart[i].product.course_namekh + "</td>";
        html += "<td>" + cart[i].product.qty_fee + "</td>";
        html += "<td>" + cart[i].product.turtion_fee + "</td>";
        html += "<td class='text-center'><input type='text' style='width:70px;' id='discount_" + cart[i].product.id + "'  class='discountCourse' value=" + cart[i].product.discount_fee + " /> &nbsp<button class='btn btn-success btn-sm deletePaymentDetail' type='button' onclick='DiscountPress(" + cart[i].product.id + ")' />Ok</button> </td>";
        html += "<td>" + cart[i].product.total_fee + "</td>";
        html += "<td>" + parseFloat(cart[i].product.turtion_fee) - parseInt(cart[i].discount_fee) + "</td>";
        if (cart[i].product.id == 0) {
            html += "<td class='text-center'><button type='button' class='btn btn-default btn-sm ' onClick='subtractQuantity(\"" + cart[i].product.id + "\", this);'/>-</button> &nbsp<button type='button' class='btn btn-default btn-sm' onClick='addQuantity(\"" + cart[i].product.id + "\", this);'/>+</button> &nbsp<button class='btn btn-danger btn-sm deletePaymentDetail' type='button' onclick='removeItem(\"" + cart[i].product.id + "\");' />X</button></td>";
        }
        else {
            html += "<td class='text-center'><button type='button' class='btn btn-default btn-sm ' onClick='subtractQuantity(\"" + cart[i].product.id + "\", this);'/>-</button> &nbsp<button type='button' class='btn btn-default btn-sm' onClick='addQuantity(\"" + cart[i].product.id + "\", this);'/>+</button> &nbsp<button class='btn btn-danger btn-sm deletePaymentDetail' type='button' onclick='removeItemFromDB(\"" + cart[i].product.id + "\");' />X</button></td>";
        }
        html += "</tr>";

        //Discount += parseFloat(cart[i].discount_fee);
        GrandTotal += parseFloat(cart[i].product.total_fee);
        
    }
    html += "<tboody>";
    document.getElementById('totalamount').innerHTML = GrandTotal;
    var rate = $("#txtrate").val();
    GrandTotalR = GrandTotal * rate;
    $("#totalamount").val(GrandTotal);
    $("#discount").val(Discount);
    $("#deposit").val(GrandTotal);
    $("#depositr").val(GrandTotalR);
    html += "</table>";
    ele.innerHTML = html;
}

function subtractQuantity(id) {
    for (var i = 0; i < cart.length; i++) {
        if (cart[i].product.id == id) {
            cart[i].product.qty_fee--;
            //cart[i].product.discount_fee;
            cart[i].product.total_fee = (cart[i].product.turtion_fee * cart[i].product.qty_fee) - (cart[i].product.turtion_fee * cart[i].product.qty_fee * cart[i].product.discount_fee / 100);
        }

        if (cart[i].product.qty_fee == 0) {
            cart.splice(i, 1);
        }

    }
    renderCartTable();
}

function addQuantity(id) {
    //alert(id);
    for (var i = 0; i < cart.length; i++) {
        if (cart[i].product.id == id) {
            cart[i].product.qty_fee++;
            cart[i].product.total_fee = (cart[i].product.turtion_fee * cart[i].product.qty_fee) - (cart[i].product.turtion_fee * cart[i].product.qty_fee * cart[i].product.discount_fee / 100)
        }
    }
    renderCartTable();
}

function removeItemFromDB(id) {
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
                        url: "/api/PaymentDetails/" + id,
                        type: "DELETE",
                        contentType: "application/json;charset=utf-8",
                        datatype: "json",
                        success: function (result) {
                            tableParrent.ajax.reload();
                            toastr.success("Payment Detail has been Deleted successfully!", "Service Response");

                            for (var i = 0; i < cart.length; i++) {
                                if (cart[i].product.id == id) {
                                    cart.splice(i, 1);
                                }
                            }
                            renderCartTable();
                        },
                        error: function (errormessage) {
                            toastr.error("This Payment Detail Cannot Delete from Database!", "Service Response");

                        }
                    });
                }
            }
        });
}

function removeItem(id) {
    for (var i = 0; i < cart.length; i++) {
        if (cart[i].product.id == id) {
            cart.splice(i, 1);
        }
    }
    renderCartTable();
}

function openInvoice(id) {
    window.open("/student-invoice/" + id, "_self")
}

function getMaxInvNo() {
    $.get("/api/Payments/?a=1&b=2", function (data, status) {
        // alert("Data: " + data + "\nStatus: " + status);
        var arrayData = data.split(',');
        //alert(arrayData[0]);
        //alert(arrayData[1]);
        $('#paymentno').val(arrayData[0]);
        $('#paymentnoformat').val(arrayData[1]);

    });
}

function LoadPaymentDetail(id) {
    $.ajax({
        url: "/api/PaymentDetails/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            for (var i = 0; i < result.length; i++) {
                var obj = result[i];
                //console.log(obj.id);
                // "paymentid": 15,
               // "courseid": 14,
                addProduct(obj.id,obj.courseid, obj.paymentid, obj.coursename, obj.coursenamekh, obj.qty, obj.turtionfee, obj.discount, obj.total)
            }

        },
        statusCode: {
            400: function () {
                toastr.error("No Course select in database.");
            }
        }
    });

}





    