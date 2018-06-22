/**
 * App Class 
 *
 * @author      Carl Victor Fontanos
 * @author_url  www.carlofontanos.com
 *
 */

/**
 * Setup a App namespace to prevent JS conflicts.
 */
var app = {

    Posts: function () {

        /**
         * This method contains the list of functions that needs to be loaded
         * when the "Posts" object is instantiated.
         *
         */
        this.init = function () {
            this.get_all_items_pagination();
        }

        /**
         * Load front-end items pagination.
         */
        this.get_all_items_pagination = function () {

            _this = this;

            /* Check if our hidden form input is not empty, meaning it's not the first time viewing the page. */
            if ($('form.post-list input').val()) {
                /* Submit hidden form input value to load previous page number */
                data = JSON.parse($('form.post-list input').val());
                _this.ajax_get_all_items_pagination(data.page, data.name, data.sort);
            } else {
                /* Load first page */
                _this.ajax_get_all_items_pagination(1, $('.post_name').val(), $('.post_sort').val());
            }

            /* Search */
            $('body').on('click', '.post_search_submit', function () {
                _this.ajax_get_all_items_pagination(1, $('.post_name').val(), $('.post_sort').val());
            });
            /* Search when Enter Key is triggered */
            $(".post_search_text").keyup(function (e) {
                if (e.keyCode == 13) {
                    _this.ajax_get_all_items_pagination(1, $('.post_name').val(), $('.post_sort').val());
                }
            });

            /* Pagination Clicks   */
            $('body').on('click', '.pagination-nav li.active', function () {
                var page = $(this).attr('p');
                _this.ajax_get_all_items_pagination(page, $('.post_name').val(), $('.post_sort').val());
            });
        }

        /**
         * AJAX front-end items pagination.
         */
        this.ajax_get_all_items_pagination = function (page, order_by_name, order_by_sort) {

            if ($(".pagination-container").length > 0 && $('.products-view-all').length > 0) {
                $(".pagination-container").html('<div class="text-center"><img src="/Images/loading.gif" class="ml-tb" height="100px" /></div>');

                var post_data = {
                    page: page,
                    search: $('.post_search_text').val(),
                    name: order_by_name,
                    sort: order_by_sort,
                    max: $('.post_max').val(),
                };

                $('form.post-list input').val(JSON.stringify(post_data));

                var data = {
                    action: 'get-all-products',
                    data: JSON.parse($('form.post-list input').val())
                };
                console.log(data);
                $.ajax({
                    url: '/TaiNan/DSTN',
                    type: 'POST',
                    data: data,
                    success: function (response) {
                        response = JSON.parse(response);

                        if ($(".pagination-container").html(response.content)) {
                            $('.pagination-nav').html(response.navigation);
                            $('.table-post-list th').each(function () {
                                /* Append the button indicator */
                                $(this).find('span.fa').remove();
                                if ($(this).hasClass('active')) {
                                    if (JSON.parse($('form.post-list input').val()).sort == 'DESC') {
                                        $(this).append(' <span class="fa fa-chevron-down pull-right textmiddle"></span>');
                                    } else {
                                        $(this).append(' <span class="fa fa-chevron-up pull-right textmiddle"></span>');
                                    }
                                }
                            });
                        }
                    }
                });
            }
        }
    }
}

/**
 * When the document has been loaded...
 *
 */
jQuery(document).ready(function () {

    posts = new app.Posts(); /* Instantiate the Posts Class */
    posts.init(); /* Load Posts class methods */

});