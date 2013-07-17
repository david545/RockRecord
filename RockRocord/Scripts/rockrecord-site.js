(function ($) {


    /*因為預設傳物件陣列時jqeury會設成data[index][attr]的格式,所以自訂ajax方法在beforeSend時將格時改成data[index].attr*/
    $.myPost = function (url, data, callback, dataType) {
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            dataType: dataType,
            success: callback,
            beforeSend: function (xhr, settings) {
                settings.data = settings.data.replace(/%5D%5B(.+?)%5D=/g, '%5D.$1=');
            }
        });
    };

    /*擴充,使綁定該包裹方法的select原素觸發click事件後會根據傳入的name和元素value組成url query來重新導向網頁*/
    $.fn.selectRedirect = function (name) {

        return this.filter('select').change(function () {

            name = name ? name : 'value';
            var value = $(this).val();
            var url = location.href;
            if (url.indexOf('?') > -1) {
                if (url.indexOf(name + '=') > -1) {
                    var reg = new RegExp('('+name+'=\\w+)|('+name+'=)');
                    location = url.replace(reg, name + '=' + value);
                } else {
                    location = url + '&' + name + '=' + value;
                }
            } else {
                location = url + '?' + name + '=' + value;
            }
        }).end();
    };

    /*建立一個簡易的對話方框,用模板Views/Shared/Dialog.cshtml*/
    $.fn.ajaxDialog= function (url,data/*function,string,object,array*/,callBack,message) {
        var settings = $.extend({
            //to do
        }, {});

 
        return this.click(function (event) {
            //若data傳入的函式則執行該函式，環境物件為呼叫該方法的包裹集合各元素,回傳值為data的值
            data = $.isFunction(data) ?($.proxy(data,this))(): data;
            $('<div>').css(
                {
                    opacity: 0.4,
                    'background-color': 'gray',
                    position: 'absolute',
                    left: 0,
                    top: 0,
                    width: $(document).width(),
                    height: $(document).height()
                }).addClass('moistBackground').appendTo('body');

            $('#dialog')
                .css(
                    {
                        position: 'absolute',
                        left: function () { return (($(window).width() - $(this).width()) / 2) + $(document).scrollLeft() },
                        top: function () { return (($(window).height() - $(this).height()) / 2) + $(document).scrollTop() },
                    }
                 )
                .find('.message')
                .text(message)
                .end()
                .show()
                .find('.cancelButton')
                .click(function () {
                    $('.moistBackground').remove();
                    $(this).closest('#dialog').hide();
                });
            $('.submitButton').click(function (event) {
                $('#dialog .message')
                    .hide()
                    .prevAll('.loadContent')
                    .show()
                    .nextAll('.buttonBar')
                    .children()
                    .attr('disabled', true);
                $.post(
                    url,
                    data,
                    function (response, status, xhr) {
                        if (xhr.status >= 200 && xhr.status < 300) {
                            ($.proxy(callBack, this))(response, status, xhr);
                            $('#dialog message')
                                .show()
                                .prevAll('.loadContent')
                                .hide()
                                .closest('#dialog')
                                .hide();
                        } else {
                            $('#dialog message')
                                .show()
                                .text('處理發生錯誤')
                                .prevAll('.loadContent')
                                .hide()
                                .nextAll('.buttonBar')
                                .children()
                                .attr('disabled', false);
                        }
                    });
            });
            event.preventDefault();

        });
    };


    
})(jQuery);