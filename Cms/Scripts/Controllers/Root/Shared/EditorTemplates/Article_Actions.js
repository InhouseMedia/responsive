define(["jquery", "dropzone", "Helpers/DragElement", "jquery-ui", "bootstrap", "Helpers/String"], function ($, Dropzone, DragElement) {
	

	


	function _setDragging() {
		Dropzone.autoDiscover = false;

		var draggable = $("table.table-drag tbody tr").draggable(
			{
				addClasses: false,
				distance: 10,
				helper: function (item) {
					var text = $(this).text().trim().capitalize();
					return DragElement(text);
				},
				containment: '#content',
				connectWith: '#content',
				connectToSortable: '#content',
				stop: function (event, ui) {
					ui.helper.addClass('loading');

					// Get render action that's been dragged into the content holder
					$.get('/Article/' + ui.helper.text() + '?Article_Id=' + $('[name=Article_Id]').val(), function (item) {


						this.helper.after($(item));
						this.helper.remove();

						// Change the levels of all content items within the holder
						$('#content').find("[name$=Level]").each(
							function (key, item) {
								$(item).val(key + 1);
							}
						);
					}.bind(ui)).error(function (item) {
						this.helper.removeClass('loading').css({ 'opacity': 0.5 })
					}.bind(ui));
				}
			}
		).disableSelection();

		var widget = draggable.data('ui-draggable');

		draggable.dblclick(function (e) {
				widget.trigger('mousedown');
			}
		);
	}

	return {
		init: function () {
			_setDragging();
		}
	};

});