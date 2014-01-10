// jQuery Context Menu Plugin
//
// Version 1.01
//
// Cory S.N. LaViska
// A Beautiful Site (http://abeautifulsite.net/)
//
// More info: http://abeautifulsite.net/2008/09/jquery-context-menu-plugin/
//
// Terms of Use
//
// This plugin is dual-licensed under the GNU General Public License
//   and the MIT License and is copyright A Beautiful Site, LLC.
//
// NOTE: modified by GE for ZFP: Added adjustMenuPosition to constrain popup menu to window
//
var isLeftMouseDown = false;

if (jQuery) (function () {
    var contextElement = null;
    var contextPosition = null;

    $.extend($.fn, {
        contextMenu: function (o, callback) {
            // Defaults
            if (o.menu == undefined) return false;
            if (o.inSpeed == undefined) o.inSpeed = 150;
            if (o.outSpeed == undefined) o.outSpeed = 75;
            // 0 needs to be -1 for expected results (no fade)
            if (o.inSpeed == 0) o.inSpeed = -1;
            if (o.outSpeed == 0) o.outSpeed = -1;
            function addSubMenus(el, menuId) {
                // add subMenus based on html definitions. (lines with class="subMenu" and subMenuId attr.)
                $('#' + menuId).find(".subMenu[subMenuId]").each(function () {
                    // subMenuId attr of menu item points to element to use as submenu
                    var subMenuId = $(this).attr("subMenuId");
                    $(this).subMenu(subMenuId, function (subaction) {
                        if ((el[0] === contextElement) && callback) {
                            // Callback
                            callback(subaction, $(contextElement), contextPosition);
                        }
                    });
                    // sub-sub menus.. doesn't work. Need to fix positioning code
                    // addSubMenus(el, subMenuId);
                });
            }
            // Loop each context menu
            $(this).each(function () {
                var el = $(this);
                var offset = $(el).offset();
                // Add contextMenu class
                $('#' + o.menu).addClass('contextMenu');
                addSubMenus(el, o.menu);
                // Simulate a true right click
                $(this).mousedown(function (e) {
                    if (isLeftMouseDown == false) {
                        var evt = e;
                        evt.stopPropagation();
                        $(this).mouseup(function () {
                            e.stopPropagation();
                            var srcElement = $(this);
                            $(this).unbind('mouseup');
                            if (evt.button === 2) {
                                contextElement = this;
                                // Hide context menus that may be showing
                                $(".contextMenu").hide();
                                // Get this context menu
                                var menu = $('#' + o.menu);

                                if ($(el).hasClass('disabled')) return false;

                                // Detect mouse position
                                var d = {}, x, y;
                                if (self.innerHeight) {
                                    d.pageYOffset = self.pageYOffset;
                                    d.pageXOffset = self.pageXOffset;
                                    d.innerHeight = self.innerHeight;
                                    d.innerWidth = self.innerWidth;
                                } else if (document.documentElement &&
                                    document.documentElement.clientHeight) {
                                    d.pageYOffset = document.documentElement.scrollTop;
                                    d.pageXOffset = document.documentElement.scrollLeft;
                                    d.innerHeight = document.documentElement.clientHeight;
                                    d.innerWidth = document.documentElement.clientWidth;
                                } else if (document.body) {
                                    d.pageYOffset = document.body.scrollTop;
                                    d.pageXOffset = document.body.scrollLeft;
                                    d.innerHeight = document.body.clientHeight;
                                    d.innerWidth = document.body.clientWidth;
                                }
                                (e.pageX) ? x = e.pageX : x = e.clientX + d.scrollLeft;
                                (e.pageY) ? y = e.pageY : y = e.clientY + d.scrollTop;
                                contextPosition = { x: x - offset.left, y: y - offset.top, docX: x, docY: y };

                                //validate the item using data binded to LI and data binded to event target before opening menu
                                if (o.validateItemData) {
                                    var dataSelector = o.selector;
                                    var rightClickTarget = $(e.target);
                                    var sourceData = validTargetData(rightClickTarget, dataSelector);

                                    if (!sourceData)
                                        return false;

                                    $(menu).find('LI').each(
                                        function () {
                                            var menuData = "";

                                            if (this.attributes["data"] != undefined)
                                                menuData = this.attributes["data"].value;
                                            
                                            if (o.validateItemData(menuData, sourceData)) {
                                                $(this).removeClass('disabled');
                                            } else {
                                                $(this).addClass('disabled');
                                            }
                                        }
                                    );
                                }

                                // Show the menu
                                $(document).unbind('click');
                                //$(menu).css({ top: y, left: x }).fadeIn(o.inSpeed);
                                adjustMenuPosition(x, y, menu, o);
                                // Hover events
                                $(menu).find('A').mouseover(function () {
                                    $(menu).find('LI.hover').removeClass('hover');
                                    $(this).parent().addClass('hover');
                                }).mouseout(function () {
                                    $(menu).find('LI.hover').removeClass('hover');
                                });



                                // Keyboard
                                $(document).keypress(function () {
                                    switch (e.keyCode) {
                                        case 38:
                                            // up
                                            if ($(menu).find('LI.hover').size() == 0) {
                                                $(menu).find('LI:last').addClass('hover');
                                            } else {
                                                $(menu).find('LI.hover').removeClass('hover').prevAll('LI:not(.disabled)').eq(0).addClass('hover');
                                                if ($(menu).find('LI.hover').size() == 0) $(menu).find('LI:last').addClass('hover');
                                            }
                                            break;
                                        case 40:
                                            // down
                                            if ($(menu).find('LI.hover').size() == 0) {
                                                $(menu).find('LI:first').addClass('hover');
                                            } else {
                                                $(menu).find('LI.hover').removeClass('hover').nextAll('LI:not(.disabled)').eq(0).addClass('hover');
                                                if ($(menu).find('LI.hover').size() == 0) $(menu).find('LI:first').addClass('hover');
                                            }
                                            break;
                                        case 13:
                                            // enter
                                            $(menu).find('LI.hover A').trigger('click');
                                            break;
                                        case 27:
                                            // esc
                                            $(document).trigger('click');
                                            break;
                                    }
                                });

                                // When items are selected
                                $('#' + o.menu).find('A').unbind('click');
                                $('#' + o.menu).find('LI:not(.disabled) A').click(function () {
                                    $(document).unbind('click').unbind('keypress');
                                    $(".contextMenu").hide();
                                    // Callback
                                    if (callback) callback($(this).attr('href').substr(1), $(srcElement), contextPosition);
                                    return false;
                                });

                                // Hide bindings
                                setTimeout(function () { // Delay for Mozilla
                                    $(document).click(function () {
                                        $(document).unbind('click').unbind('keypress');
                                        $(menu).fadeOut(o.outSpeed);
                                        return false;
                                    });
                                }, 0);
                            }
                        });
                    }
                });

                // Disable text selection
                if ($.browser.mozilla) {
                    $('#' + o.menu).each(function () { $(this).css({ 'MozUserSelect': 'none' }); });
                } else if ($.browser.msie) {
                    $('#' + o.menu).each(function () { $(this).bind('selectstart.disableTextSelect', function () { return false; }); });
                } else {
                    $('#' + o.menu).each(function () { $(this).bind('mousedown.disableTextSelect', function () { return false; }); });
                }
                // Disable browser context menu (requires both selectors to work in IE/Safari + FF/Chrome)
                $(el).add($('UL.contextMenu')).bind('contextmenu', function () { return false; });

            });
            return $(this);

            function validTargetData(target, dataSelector) {
                if (canRetrieveDataBySelector(target, dataSelector))
                    return target.closest(dataSelector)[0].attributes["data"].value;
                else if (canRetrieveAttributes(target))
                    return target.attributes["data"].value;

                return "";
            }

            function canRetrieveDataBySelector(target, dataSelector) {
                return dataSelector && target.closest(dataSelector)[0] && target.closest(dataSelector)[0].attributes;
            }

            function canRetrieveAttributes(target) {
                return target && target.attributes;
            }

            function adjustMenuPosition(x, y, menu, o) {
                var docWidth = $(window).width();
                var docHeight = $(window).height();
                var menuHeight = menu.height();
                var menuWidth = menu.width();
                var posTop = y;
                var posLeft = x;
                var calculatedWidth = docWidth - menuWidth;
                var calculatedHeight = docHeight - menuHeight;

                if (x >= calculatedWidth && y <= calculatedHeight) {  //case rightTop corner 
                    posLeft = calculatedWidth - 10;
                }
                else if (x >= calculatedWidth && y >= calculatedHeight) {//case rightBottom corner 
                    posTop = calculatedHeight - 10;
                    posLeft = calculatedWidth - 10;
                }
                else if (x <= menuWidth && y >= calculatedHeight) {  //case leftBottom corner 
                    posTop = calculatedHeight - 10;
                    posLeft = 10;
                }
                else if (x > menuWidth && y >= calculatedHeight && x < calculatedWidth) {// case middleBottom corner 
                    posTop = calculatedHeight - 10;
                }
                $(menu).css({ top: posTop, left: posLeft }).fadeIn(o.inSpeed);

                alert("Menu Displayed");
            }
        },

        // Disable context menu items on the fly
        disableContextMenuItems: function (o) {
            if (o == undefined) {
                // Disable all
                $(this).find('LI').addClass('disabled');
                return ($(this));
            }
            $(this).each(function () {
                if (o != undefined) {
                    var d = o.split(',');
                    for (var i = 0; i < d.length; i++) {
                        $(this).find('A[href="' + d[i] + '"]').parent().addClass('disabled');

                    }
                }
            });
            return ($(this));
        },

        // Enable context menu items on the fly
        enableContextMenuItems: function (o) {
            if (o == undefined) {
                // Enable all
                $(this).find('LI.disabled').removeClass('disabled');
                return ($(this));
            }
            $(this).each(function () {
                if (o != undefined) {
                    var d = o.split(',');
                    for (var i = 0; i < d.length; i++) {
                        $(this).find('A[href="' + d[i] + '"]').parent().removeClass('disabled');

                    }
                }
            });
            return ($(this));
        },

        // Disable context menu(s)
        disableContextMenu: function () {
            $(this).each(function () {
                $(this).addClass('disabled');
            });
            return ($(this));
        },

        // Enable context menu(s)
        enableContextMenu: function () {
            $(this).each(function () {
                $(this).removeClass('disabled');
            });
            return ($(this));
        },

        // Destroy context menu(s)
        destroyContextMenu: function () {
            // Destroy specified context menus
            $(this).each(function () {
                // Disable action
                $(this).unbind('mousedown').unbind('mouseup');
            });
            return ($(this));
        },

        // unbind context menu
        unbindContextMenu: function (menuId) {
            $(this).each(function () {
                // main bindings for popup
                $(this).unbind('mousedown').unbind('mouseup');
            });
            // now unbind all subMenu item clicks.
            $('#' + menuId).find(".subMenu[subMenuId]").each(function () {
                var subMenuId = $(this).attr("subMenuId");
                $('#' + subMenuId).find('li a').unbind('click');
            });
            return ($(this));
        },


        createSubMenu: function (obj, objJson, callBack) {
            var presetItems = [];
            var ul = $('#rcSubMenu');
            if ($(this).hasClass('disabled')) {
                $('#rcSubMenu').attr("style", "visibility: hidden");
                return false;
            }
            $(this).each(function () {
                $(this).hover(function () {
                    if (!$(this).hasClass('disabled')) {
                        $('#rcSubMenu').removeAttr("style");
                    }
                    adjustSubMenuPosition(ul, $(this));
                    $(ul).mouseover(function () {
                        $(ul).show();
                        $('.zoompreset').addClass('hover');
                    });
                    $(ul).mouseout(function () {
                        $(ul).hide();
                        $('.zoompreset').removeClass('hover');
                    });
                },
                    function () {
                        $(ul).hide();
                    }
                );
            });
            return ($(this));
        },

        subMenu: function (id, subMenuCallback) {
            var menuUl = $('#' + id);
            $(this).each(function () {
                var $subMenuParent = $(this);
                menuUl.find('li a').click(function () {
                    if (!$(this).attr('href')) {
                        console.error("no href in clicked submenu. menu=" + this);
                    } else {
                        subMenuCallback($(this).attr('href').replace("#", ""));
                    }
                    menuUl.hide();
                });
                $(this).hover(function () {
                    adjustSubMenuPosition(menuUl, $(this));
                    $(menuUl).unbind('mouseover');
                    $(menuUl).unbind('mouseout');
                    $(menuUl).mouseover(function () {
                        $(menuUl).show();
                        $(menuUl).addClass('hover');
                        $subMenuParent.addClass('hover');
                    });
                    $(menuUl).mouseout(function () {
                        $(menuUl).hide();
                        $(menuUl).removeClass('hover');
                        $subMenuParent.removeClass('hover');
                    });
                },
                    function () {
                        $(menuUl).hide();
                    }
                );
            });
        }
    });

    function adjustSubMenuPosition(ul, menuLi) {

        var docWidth = $(window).width();
        var docHeight = $(window).height();
        var menuHeight = ul.height();
        var menuWidth = ul.width();
        var topPos = menuLi.offset().top;
        var leftPos = menuLi.offset().left;
        var width = menuLi.outerWidth();
        var calTop = topPos - 1;
        var calLeft = leftPos + 1 + width;

        if ((docHeight - topPos) < menuHeight) {
            calTop = menuLi.closest('ul').offset().top + menuLi.closest('ul').height() - menuHeight;
        }
        if ((docWidth - leftPos - width) < menuWidth) {
            calLeft = leftPos - menuWidth;
        }
        $(ul).css({ "left": (calLeft) + "px", "top": (calTop) + "px", "position": "absolute", "border-left": "1px" }).show();
    }
})(jQuery);

