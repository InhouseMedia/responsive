define(['jquery', 'Helpers/DragElement', 'jquery-ui', 'bootstrap'], function ($, DragElement) {
	"use strict";
	//console.log("article/create loaded");



	function _setSortable() {
		$("#content").sortable(
			{
				addClasses: false,
				distance: 10,
				axis: 'y',
				containment: '#content',
				connectWith: '#content',
				receive: function (event, ui) {
					ui.helper.first().removeAttr('style').removeClass('.ui-sortable-helper'); // undo styling set by jqueryUI
				},

				helper: function (event, ui) {
					var text = $(ui).find('.panel-heading').text();
					return DragElement(text);
				},

				start: function (event, ui) {
					//todo: can't dragg a collapsed content box directly downwards
					var panels = $(this).find('.collapse.in');

					var heightList = $(this).find('.panel:not(.deleted)').map(function (key, item) { return $(item).outerHeight(); });
					heightList.push(40); // needed when you want to sort when all panels are open.(specify a default small height)
					var height = Math.min.apply(Math, heightList);

					$(this).find('.ui-sortable-placeholder').css({ 'height': height + 'px' });
				},
				stop: function (event, ui) {
					var panels = $(this).find('.tempCollapse .collapse');
					panels.collapse('show');
					panels.closest('.tempCollapse').removeClass('tempCollapse');

					// is used when a new article is created and the content droppable holder is empty 
					$(this).removeClass('empty');

					// Change the levels of all content items within the holder
					$(this).find("[name$=Level]").each(
						function (key, item) {
							$(item).val(key + 1);
						}
					);
				}
			}
		).droppable(
			{
				activeClass: 'highlight'
			}
		).disableSelection();
	}

	return {
		init: function () {
			_setSortable();
		}
	}
});