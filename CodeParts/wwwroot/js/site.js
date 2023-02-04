//function OpenModelWindow(parametrs) {
//    const url = parametrs.url;
//    const codeCount = parametrs.data;
//    const modal = $('#modal');
//    if (url == undefined || codeCount == undefined) {
//        alert('model or url undefined')
//    }
//    $.ajax({
//        type: 'GET',
//        url: url,
//        data: { "codeCount": codeCount },
//        success: function (response) {
//            modal.find(".modal-body").html(response);
//            modal.modal('show')
//        },
//        failure: function () {
//            modal.modal('hide')
//        },
//        error: function (response) {
//            alert(response.responseText);
//        }
//    });
//}