define(['jquery', 'bootstrap'], function ($) {
	"use strict";

	// Show Model (lightbox)
	function _showModal() {
		$('[data-toggle=modal]').click(function (e) {
			e.preventDefault();
			e.stopPropagation();
			// Check if modal should be large or small
			var btn = $(this);
			var modalName = btn.attr('data-target');
			var smallModal = (modalName.search('Small') != -1);
			// Show loading icon in button
			btn.addClass("submit");

			$.get(this.href, function (data) {
				$('body').append(data);
				btn.removeClass('submit');
				$(modalName)
					.on('hidden.bs.modal', function () { $(this).remove(); })
					.on('show.bs.modal', function () { if (smallModal) $(this).find('.modal-dialog').addClass('modal-sm'); })
					.on('shown.bs.modal', function () { $(this).find('[autofocus]').focus(); }) // Set Autofocus to inputfield in modals 
					.modal({ show: true, keyboard: true, backdrop: true })
			});
			return false;
		});
	}

	// Show tooltips
	function _showTooltip() {
		$('[data-toggle="tooltip"]').tooltip(
			{
				'trigger': 'hover',
				'animation': true,
				'delay': { 'show': 500, 'hide': 1000 }
			}
		);
	}

	return {
		init: function () {
			console.info('require _Layout fired');
			_showModal();
			_showTooltip();
		}
	}
});