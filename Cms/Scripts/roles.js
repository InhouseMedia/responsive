var timeoutObj;
var progressBar = $(".navbar .progress .progress-bar");

$("#userForm input[type=checkbox]").change(
	function (item) {
		clearTimeout(timeoutObj);
		progressBar.removeClass("countDown")
			.removeClass("loaded")
			.removeClass("progress-bar-danger");
		timeoutObj = setTimeout(timeout, 4000);
		setTimeout(function(){progressBar.addClass("countDown")
		.addClass("progress-bar-info")},0);
	}
);

function timeout() {
	progressBar.removeClass("countDown")
		.addClass("progress-bar-striped")
		.addClass("active")
		.addClass("loading");
	$('#userForm').submit();
}

function onSuccess(ajaxContext) {
	progressBar.removeClass("progress-bar-striped")
		.removeClass("active")
		.removeClass("loading")
		.addClass("loaded")
	setTimeout(function () { progressBar.removeClass("loaded");}, 300);
}

function onFailure(ajaxContext) {
	progressBar.removeClass("progress-bar-striped")
		.removeClass("active")
		.removeClass("loading")
		.addClass("loaded")
		.addClass("progress-bar-danger");
	setTimeout(function () {
		progressBar.removeClass("loaded");
		setTimeout(function () {
			progressBar.removeClass("progress-bar-danger");
		},1500);
	}, 3000);
	
}