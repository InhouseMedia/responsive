define(['jquery', 'bootstrap', 'bootstrap-slider', 'jquery-serializeJSON'], function ($) {
	"use strict";
	var imageChangeTimeout = 0;
	var panel;
	var maxFilesize;
	var dictDefaultMessage;
	
	function _loadDropZone() {
		panel.find('.dropzone').dropzone(
			{
				url: '/File/SaveImage',
				maxFilesize: maxFilesize,
				maxFiles: 1,
				acceptedFiles: 'image/*',
				dictDefaultMessage: dictDefaultMessage
			}
		);
	}

	function _bootstrapSlider() {
		panel.find('.slider').bootstrapSlider();
	}

	function _triggerImageChange(){
		panel.on('change', 'input[name*=imageConfig]', _executeImageChangeTimeout);
	}

	function _executeImageChangeTimeout() {
		window.clearTimeout(imageChangeTimeout);
		imageChangeTimeout = window.setTimeout(_executeImageChange.bind(panel), 350);
	}

	function _executeImageChange(e){
		var imageOptions = {};
		var img = this.find('img').get(0);

		if (img == null) return;	

		var jsonOptions = $(this).find('input[type!=hidden][name^=imageConfig]').serializeJSON();
			jsonOptions['imageConfig.custom'] = true;

		var mapping = $.each(jsonOptions, function (key, item) {
			key = (item === 'sepia') ? 's_sepia' : key;
			item = (item === 'sepia') ? true : item;
			imageOptions[key] = (Array.isArray(item)) ? item.join(',') : item;
		});

		var patternDot = new RegExp('\\_', 'gm');
		var patternName = new RegExp('imageConfig\.', 'gmi');
		var patternArray = new RegExp('\[\]', 'gm');

		var urlQuery = $.param(imageOptions).replace(patternName, '').replace(patternDot, '.').replace(patternArray, '');

		console.log(imageOptions, urlQuery);

		img.src = img.src.split("?")[0] + "?" + urlQuery;
	}

	function _removePanel() { 
		$('#content').on('click', 'button.glyphicon-trash',
			function (e) {
				var panel = $(this).closest('.panel');
				// When an contentbox is deleted before it is saved, it should be removed otherwise
				// it should be marked as deleted so that the database can remove it.
				if (panel.find('input[name$=Id]').val() == 0) {
					panel.remove();
				} else {
					panel.addClass('deleted');
					panel.find('input[name$=Active]').val(4); // is used when an item should be deleted
				}

				var content = $("#content");

				if (content.find(".panel:not(.deleted)").length == 0) {
					content.addClass('highlight empty');
				}

				e.stopPropagation();
				return false;
			}
		)
	}


	function _inputfieldPanel() {
		$('#content').on('click', '.panel-heading .form-control',
			function (e) {
				var header = $(this).closest('.panel-heading.collapsed');

				if (header.length > 0) {
					e.preventDefault();
					this.blur();
				} else {
					this.focus();
					e.stopPropagation();
					return false;
				}
			}
		);
	}

	return {
		start: function () {
			_loadDropZone();
			_bootstrapSlider();
			_triggerImageChange();
			_removePanel();
			_inputfieldPanel();
		},
		model: function (modelId) { panel = $('#collapseImage_' + modelId); },
		message: function (text) { dictDefaultMessage = text; },
		maxfilesize: function (size) { maxFilesize = size; }
		
	}
});
