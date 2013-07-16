        $(function () {
            $('[name*="Amount"]', '#cartList')
                .keydown(function (event) {
                    //限制使用者只能輸入數字
                    if ((event.which < 96 || event.which > 105 || $(this).val().length >= 3) &&
                         event.which != 8 && event.which != 46 &&
                         event.which != 37 && event.which != 39) {
                        event.preventDefault();
                    }
                })
                .blur(function (event) {
                    if (!/^\d+$/.test($(this).val())) {
                        $(this).val(1);
                    }
                });

            $('#updateAmountButton').click(function (event) {
                    
                $(event.target).attr('disabled', true);
                $('#cartList').css('opacity', 0.5);
                var cartItemDatas = $('.cartList-item').map(function () {
                    return {
                        Id: $('[name*="Id"]', this).val(),
                        Amount: $('[name*="Amount"]', this).val()
                    };
                }).toArray();
                $.myPost('/Cart/UpdateAmount',
                        { updateCartItems: cartItemDatas },
                        function (result, status, xhr) {
                            $(event.target).attr('disabled', false);
                            $('#cartList').css('opacity', 1);
                            if (!result.Error) {
                                $('.cartList-item [name*="Id"]').each(function () {
                                    var newItem = getNewItem(result.NewCartItems, $(this).val());

                                    $(this).closest('tr')
                                           .find('.cart-item-price')
                                           .text(newItem.Price)
                                           .end()
                                           .find('[name*="Amount"]')
                                           .val(newItem.Amount);
                                });
                                $('.cartTotal').text(result.Total);
                                changeCartLinkText();
                            } else {
                                $('#errorMessageContent').text(result.Error);
                            }
                        },
                        'json');
            });

            $('.cartItemRemoveButton').click(function (event) {
                var itemRow$ = $(event.target).hide()
                                .siblings('.ajaxLoader')
                                .show()
                                .closest('.cartList-item')
                                .css('opacity',0.5);

                $.post('/Cart/Delete',
                        'id='+ itemRow$.find('[name*="Id"]').val(),
                        function (respone, status, xhr) {
                            if (xhr.status >= 200 && xhr.status < 300) {
                                
                                itemRow$.fadeOut('slow', function () {
                                    $(this).remove();
                                    changeCartLinkText();
                                });
                                
                            }
                            else {
                                $(event.target).show();
                                $('#errorMessageContent').text('刪除失敗');
                            }
                        },
                        'json');
                
                event.preventDefault();
            });

            function getNewItem(newCartItems,id) {
                for (var index in newCartItems) {
                    if (newCartItems[index].Id == id) {
                        return newCartItems[index];
                    }
                }
            }

            function changeCartLinkText() {

                var totlaAmount = 0;
                $('.cartList-item [name*="Amount"]').each(function () {
                    totlaAmount +=new Number($(this).val());
                });

                if (totlaAmount > 0) {
                    $('#cartLink').text('購物車(' + totlaAmount + ')');
                } else {
                    $('#cartLink').text('購物車');
                    $('#cartContent').html($('<p>').addClass('notContent').text('您的購物車目前沒有任何商品'));
                }
            }
        });
