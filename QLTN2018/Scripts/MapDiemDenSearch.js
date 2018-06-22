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

        var per_page = 20;
        var mForm = "myForm";
        var listDiemDen = [];
        /**
         * This method contains the list of functions that needs to be loaded
         * when the "Posts" object is instantiated.
         *
         */
        this.init = function () {

            this.submit_map_search();
            loadDiemDen();
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

                $.ajax({
                    url: '/TaiNan/MapDiemDenSearch',
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

        this.submit_map_search = function() {
            _this = this;
          
            /* Check if our hidden form input is not empty, meaning it's not the first time viewing the page. */
            if ($('form.post-list input').val()) {
                /* Submit hidden form input value to load previous page number */
                page = $('form.post-list input.page').val();
                data = $('form.post-list input.data').val();
                _this.ajax_submit_map_search(page, data, "get-page-items");
            } else {
                /* Load first page */
                var formData = $('#' + mForm).serialize();
                _this.ajax_submit_map_search(1, formData, "get-all-items");
            }
            //_this.ajax_submit_map_search();

            /* Search */
            $('body').on('click', '.post_search_submit', function (e) {
                e.preventDefault();
                var formData = $('#' + mForm).serialize();
                _this.ajax_submit_map_search(1, formData, "get-all-items");
            });
            /* Search when Enter Key is triggered */
            $(".post_search_text").keyup(function (e) {
                if (e.keyCode == 13) {
                    var formData = $('#' + mForm).serialize();
                    _this.ajax_submit_map_search(1, formData, "get-all-items");
                }
            });
            $('body').on('click', '.pagination-nav li.active', function () {
                var page = $(this).attr('p');
                var data = $('form.post-list input.data').val();
                _this.ajax_submit_map_search(page, data, "get-page-items");
            });
           
        }

        this.ajax_submit_map_search = function (page, fData, action) {
            $('form.post-list input.page').val(page);
            $('form.post-list input.data').val(fData);

            var data = fData + "&Page=" + page + "&UpdateZone=" + action;
            $(".pagination-container").html('<div class="text-center"><img src="/Images/loading.gif" class="ml-tb" height="100px" /></div>');

            if (action === "get-all-items") {
                deleteMarkers();
            }
            $.ajax({
                url: "/TaiNan/MapDiemDenSearch",
                type: "GET",
                data: data,

                success: function (response) {
                   
                    var newRow = '<div class="table-responsive">' +
                        '<table class="table table-striped table-bordered table-condensed table-hover js-table" style="margin-bottom:10px">' +
                        '<thead><tr><th>Số BC</th><th>Thời gian</th><th>Loại</th><th>Đơn vị</th><th>Địa chỉ</th>' +
                        '<th>Nguyên nhân</th><th>Phương tiện</th><th>HH</th><th>BT</th><th>TV</th><th></th></tr></thead><tbody>';
                    var hasData = 0;
                    if (response.updateZone ==="All"){
                      
                        var bounds = new google.maps.LatLngBounds();
                        var infowindow = new google.maps.InfoWindow();

                        $.each(response.allitems, function (i, tn) {
                            if (CheckInside(tn.Lat,tn.Lng,"K")) {
                                var $description = $('<div/>');
                                $description.append($('<p class="pull-right" />').html('<b >Ngày: </b>' + formatDate(tn.TGTN)));
                                $description.append($('<p/>').html('<b>Số báo cáo: </b>' + tn.SoBC));

                                $description.append($('<p/>').html('<b>Hình thức va chạm: </b>' + tn.TenHTVC));
                                if (tn.TenDTNNTNGT != null) {
                                    $description
                                        .append($('<p/>').html('<b>Nguyên nhân: </b>' + tn.TenDTNNTNGT));
                                };
                                if (tn.TenNNTNGT != null) {
                                    $description
                                        .append($('<p/>').html('&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp' + tn.TenNNTNGT));
                                };
                                $description.append($('<p/>').html('<b>Loại đường: </b>' + tn.TenLD));
                                $description.append($('<p/>').html('<b>Hư hỏng: </b>' + tn.SoHH + "&nbsp&nbsp&nbsp&nbsp&nbsp" + '<b>Bị thương: </b>' + tn.SoBT + "&nbsp&nbsp&nbsp&nbsp&nbsp" + '<b>Tử vong: </b>' + tn.SoTV));
                                if (tn.TomTatSoBo != null) {
                                    $description
                                        .append($('<p/>').html('<b>Tóm tắt sơ bộ: </b>' + tn.TomTatSoBo));
                                };

                                var contentString = $description.html();
                                marker = new google.maps.Marker({
                                    map: map,
                                    position: new google.maps.LatLng(tn.Lat, tn.Lng),

                                    icon: iconset(tn.SoTV)
                                });

                                marker.addListener('click', function () {
                                    infowindow.setContent(contentString);
                                    infowindow.open(marker.get('map'), this);
                                });



                                markers.push(marker);
                                bounds.extend(marker.getPosition());
                                map.fitBounds(bounds);

                                //Table

                                newRow += '<tr>' +
                                     '<td>' + tn.SoBC + '</td>' +
                                     '<td class="width-10">' + formatDate(tn.TGTN) + '</td>' +
                                     '<td class="width-12">' + tn.TenLTN + '</td>' +
                                     '<td class="width-10">' + tn.TenDVCTN + '</td>' +
                                     '<td>' + tn.DiaChi + '</td>' +
                                     '<td>' + tn.TenDTNNTNGT + '</td>' +
                                     '<td>' + tn.TenPT + '</td>' +
                                     '<td>' + tn.SoHH + '</td>' +
                                     '<td>' + tn.SoBT + '</td>' +
                                     '<td>' + tn.SoTV + '</td>' +
                                    '<td>' + '<a onclick="clickroute(' + tn.Lat + ',' + tn.Lng + ');infoWindow(' + hasData + ');" href="#PanelMap" data-id=' + tn.TaiNanID + '><span data-id=' + tn.TaiNanID + ' ' +
                                     'class="fa fa-location-arrow fa-lg" aria-hidden="true" style="color:#151fa9; margin: 3px;cursor: pointer;"></span>' +
                                     '</a>' +
                                     '</td>' +
                                    '</tr>';
                                hasData ++;
                            }
                        });
                    }
                    newRow += '</tbody></table></div>';
                    if (hasData>0) {
                        $('.pagination-container').html(newRow);
                    }
                    else {
                        $('.pagination-container').html("<p class='p-d bg-danger'>No items found</p>");
                    }
                    //if (response.pageitems.length>0){
                    //    $.each(response.pageitems,
                    //        function(i, tn) {
                    //            newRow += '<tr>' +
                    //                 '<td>' + tn.SoBC + '</td>' +
                    //                 '<td class="width-10">' + formatDate(tn.TGTN) + '</td>' +
                    //                 '<td class="width-12">' + tn.TenLTN + '</td>' +
                    //                 '<td class="width-10">' + tn.TenDVCTN + '</td>' +
                    //                 '<td>' + tn.DiaChi + '</td>' +
                    //                 '<td>' + tn.TenDTNNTNGT + '</td>' +
                    //                 '<td>' + tn.TenPT + '</td>' +
                    //                 '<td>' + tn.SoHH + '</td>' +
                    //                 '<td>' + tn.SoBT + '</td>' +
                    //                 '<td>' + tn.SoTV + '</td>' +
                    //                 '<td>' + '<a onclick="clickroute(' + tn.Lat + ',' + tn.Lng + ');infoWindow(' + ( (page-1)*per_page + i) + ');" href="#PanelMap" data-id=' + tn.TaiNanID + '><span data-id=' + tn.TaiNanID + ' ' +
                    //                 'class="fa fa-location-arrow fa-lg" aria-hidden="true" style="color:#151fa9; margin: 3px;cursor: pointer;"></span>' +
                    //                 '</a>' +
                    //                 '</td>' +
                    //                 '</tr>';

                            
                    //        });
                    //    newRow += '</tbody></table></div>';
                    //    $('.pagination-container').html(newRow);
                    //}
                    //else { 
                        
                    //    $('.pagination-container').html("<p class='p-d bg-danger'>No items found</p>");
                    //}
                    //$('.pagination-nav').html(response.pag_navigation);
                    
                  

                },
                error: function (error) {
                    console.log(error);
                    alert(error);
                }
            });
        }




        function CheckInside(lat1, lng1, unit)
        {
            var result = false;
            $.each(listDiemDen, function (i, dd) {
                var radlat1 = Math.PI * lat1 / 180;
                var radlng1 = Math.PI * lng1 / 180;
                var radlat2 = Math.PI * dd.Lat / 180;
                var radlng2 = Math.PI * dd.Lng / 180;
               
                var theta = lng1 - dd.Lng;
                var radtheta = Math.PI * theta / 180;
                var dist = Math.sin(radlat1) * Math.sin(radlat2) + Math.cos(radlat1) * Math.cos(radlat2) * Math.cos(radtheta);
                dist = Math.acos(dist);
                dist = dist * 180 / Math.PI;
                dist = dist * 60 * 1.1515;
                if (unit == "K") { dist = dist * 1.609344; }
                if (unit == "N") { dist = dist * 0.8684; }
                console.log(dist * 1000);
                console.log( dd.Radius);
                if (dist*1000 <= dd.Radius) result = true;
            });

            return result;
           
        }

        function loadDiemDen() {
            return $.ajax({
                type: "GET",
                url: "/diemden/GetListDiemDen",
                dataType: "json",
                success: function (response) {
                    listDiemDen = response.Data;
                   

                    var circles = [];
                    $.each(listDiemDen, function (i, dd) {
                    circles[i] = new google.maps.Circle({
                            fillColor: '#AA0000',
                            fillOpacity: 0.2,
                            strokeWeight: 1,
                            clickable: false,
                            editable: false,
                            draggable: false,
                            map: map,
                            radius: dd.Radius,
                            center: new google.maps.LatLng(dd.Lat, dd.Lng),
                            zIndex: 1
                        });
                    });
                }
            });
        }
    }
}

/**
 * When the document has been loaded...
 *
 */
jQuery(document).ready(function () {
    
    posts = new app.Posts(); 
    posts.init();
});