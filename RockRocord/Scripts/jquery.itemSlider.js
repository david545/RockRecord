(function ($) {
    $.fn.itemSlider = function (options) {
        var settings = $.extend({
            itemCount: 5,
            itemWidth: 150,
            itemHeight: 230,
            itemMargin: 10,
            hoverIn: function () { $(this).toggleClass('hover'); },
            hoverOut: function () { $(this).toggleClass('hover'); }
        }, options || {})

        function getSliderWidth() {
            return settings.itemCount * (settings.itemWidth + settings.itemMargin);
        }

        if (isNaN(new Number(settings.itemCount))) {
            throw new Error("itemCount必須是數字");
        }
        if (isNaN(new Number(settings.itemWidth))) {
            throw new Error("itemWidth必須是數字");
        }
        var context = $(this).filter('ul').first();
        if (context.size() <= 0) return this;
        var current = 0;
        var total = context.find('li').size();

        var sliderLayout = $('<div>')
            .css({
                overflow: 'hidden',
                'margin-left': '68px'
            })
		   .width(getSliderWidth())
           .wrap($('<div>').css('position', 'relative').addClass('sliderLayout').width(getSliderWidth()))
           .closest('div.sliderLayout');
        context.addClass('sliderItemList')
               .css({
                   position: 'relative',
                   height: settings.itemHeight
               })
               .wrap(sliderLayout)
               .find('li')
               .each(function (index) {
                   $(this).css({
                       left: index * (settings.itemWidth + settings.itemMargin) + settings.itemMargin,
                       position: 'absolute',
                       width: settings.itemWidth,
                       height:settings.itemHeight,
                       display: 'inline-block'
                   })
               })
               .hover(settings.hoverIn,settings.hoverOut)
               .closest('div.sliderLayout')
               .append(createArrow());

        function createArrow() {
            return $('<img>', { src: '/Content/prevArrow.png' })
			            .css('left', '0px')
						.click(function () {
						    if (current - settings.itemCount <= 0) return;
						    current -= settings.itemCount;
						    context.animate({ 'margin-left': '+=' + getSliderWidth() }, 'normal');
						})
                        .add(
                            $('<img>', { src: '/Content/nextArrow.png' })
			                    .css('left', getSliderWidth() + 55)
						        .click(function () {
						            if ((current - total) >= 0) return;
						            current += settings.itemCount
						            context.animate({ 'margin-left': '-=' + getSliderWidth() }, 'normal');
						        })
                        )
                        .hover(function () {
                            if ($(this).toggleClass('hover')
								   .hasClass('hover')) {
                                $(this).css('background-color', '#D3D3D3');
                            } else {
                                $(this).css('background-color', 'transparent');
                            }

                        })
                        .css({
                            position: 'absolute',
                            top: '40%',
                            cursor: 'pointer'
                        });
        }

        current += settings.itemCount;
        return this;
    };
})(jQuery);