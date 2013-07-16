$(function () {
    $('.orderStatus-button-save').live('click', function () {
        var context = $(this).closest('td.orderStatus');
        var statusValue =
               $(this).closest('div.orderStatus-button-content')
               .find(':button')
               .attr('disabled', true)
               .end()
               .siblings('.ajaxLoader')
               .show()
               .siblings('.orderStatusSelect-content')
               .find('select')
               .val();
        var orderId = context.closest('tr').attr('data-orderId');
        $.post('/Order/UpdateOrderStauts/' + orderId,
               'status=' + statusValue,
               function (response, textStatus, xhr) {
                   if (xhr.status >= 200 && xhr.status < 300) {
                       showNormalModel(context);
                       $('.orderStatusName', context).text(response);
                   }
               },
               'text');

    });

    $('.orderStatus-button-cancel').live('click', function () {
        showNormalModel($(this).closest('td.orderStatus'));
    });

    function showNormalModel(orderStatusTd) {
        $('.orderStatusSelect-content', orderStatusTd)
            .empty()
            .siblings('div.orderStatus-button-content,.ajaxLoader')
            .hide()
            .find(':button')
            .attr('disabled', false)
            .end()
            .siblings('.orderStatusName,.orderStatusSelectLink')
            .show();
    }

    $('.manage-delete').each(function () {
        $(this).ajaxDialog('/Order/Delete/' + $(this).closest('tr').attr('data-orderId'),
                           null,
                           function (respone, state, xhr) {
                               location.href = location.href;
                           },
                           '確定要刪除該訂單?');
    });

    $('.orderStatusSelectLink').click(function (e) {
        var url = $(this).hide()
                         .siblings('img.ajaxLoader')
                         .show()
                         .end()
                         .attr('href');
        $(this).siblings('.orderStatusSelect-content')
               .load(url, function () {
                   $(this).siblings('.orderStatusName,.ajaxLoader')
                          .hide()
                          .siblings('.orderStatus-button-content')
                          .show();
               });
        e.preventDefault();
    });
})