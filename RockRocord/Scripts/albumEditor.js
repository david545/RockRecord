/********************提供給AlbumEditorTemplate.cshtml部分檢視用*********************************/

//歌曲的輸入欄位預先榜定事件
//要輸入的欄位預設會提供提示，提示字串放在每個元素的data-tip，若失去焦點還沒輸入資料，則繼續顯示提示字串
$('#songEditTable input')
    .live('focus', function () {
        if ($(this).hasClass('empty')) {
            $(this).val('');
            $(this).removeClass('empty');
        }
    })
    .live('blur', function () {
        if (!$(this).val()) {
            $(this).addClass('empty');
            $(this).val($(this).attr('data-tip'));
        }
        else if ($(this).hasClass('songLengthInput') && !$(this).hasClass('empty')) {
            //檢查歌曲長度是否符合格式mm:ss
            if (!/^\d{2}:\d{2}$/.test($(this).val())) {
                $(this).val('00:00');
            }
        } else if ($(this).hasClass('songNumberInput')) {
            if (!/^\d+$/.test($(this).val())) {
                $(this).val(1);
            }
        }
    });
$('.songEditRow').live('adjustSuffix', function () {
    $(this)
        .nextAll('tr')
        .find('input')
        .attr('name', function () {
            var reg = /Album\.Songs\[(\d+)\]/;
            if (!reg.test($(this).attr('name'))) return $(this).attr('name');
            var result = $(this).attr('name').match(reg);
            var newSuffix = result[1] - 1;
            var property = $(this).attr('data-property');
            return $(this).attr('name')
                          .replace(/(Album\.Songs\[)\d+(\])/, '$1' + newSuffix + '$2');
        });

});

$('#songEditTable .removeButton').live('click', function (event) {

    //目前索引需要-1
    $('#songEditTable').data('suffix', $('#songEditTable').data('suffix') - 1);

    //移除後去處發自訂事件adjustSuffix去更改每個input的索引,使得它的索引是連續的
    $(this)
        .closest('tr.songEditRow')
        .trigger('adjustSuffix')
        .fadeOut('normal', function () {
            $(this).remove();
        });


    event.preventDefault();

});



$(function () {

    //因為在資料庫設計歌曲的長度是設計成秒數，所以是int，但在用戶端我們讓使用者輸入格式mm:ss的字串,所以送出前要先轉成秒數
    $('#submitButton').click(function () {
        $('#songEditTable input').each(function () {
            //若使用者沒輸入資料則略掉
            if ($(this).hasClass('empty')) {
                $(this).closest('tr.songEditRow').trigger('adjustSuffix').remove();
            } else if ($(this).hasClass('songLengthInput')) {
                var result = $(this).val().match(/^(\d{2}):(\d{2})$/);
                var minTosec = result[1] * 60;
                $(this).val(new Number(result[2]) + minTosec);
            }
        });
    });


    //把目前資料列的索引封裝在songEditTable裡
    //為了要每個欄位有獨一無二的索引，這樣送到伺服器端才能判斷哪個值屬於哪個欄位
    $('#songEditTable').data('suffix', $('#songEditTable tr.songEditRow').size());

    $('.createSongButton').click(function (event) {
        insertRow();
        event.preventDefault();
    });

    function insertRow() {
        var suffix = $('#songEditTable').data('suffix');
        $('#songEditTable').data('suffix', suffix + 1);
        $('#songRowTemplate tr:first')
            .clone()
            .appendTo('#songEditTable tbody')
            .find('input')
            .attr('name', function () {
                //把name屬行改成ModelBuilder可以綁定的名稱
                var property = $(this).attr('data-property');
                $(this).attr('name', 'Album.Songs[' + suffix + '].' + property)
            });

    }

    $('#Album_PublicDate').datepicker(
        {
            yearRange: 'c-90:c+90',
            changeYear: true,
            changeMonth: true,
            dateFormat: 'yy/mm/dd'
        });

    if ($('#songEditTable .songEditRow').size() <= 0) {
        insertRow();
    }
});