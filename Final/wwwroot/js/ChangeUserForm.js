$("#changeUserForm").submit(function (event) {
	event.preventDefault
	$('#Password').val(sha512($('#Password').val()))
});