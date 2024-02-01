function lockModal(truefalse) {
    $("#uploadFileModal").find("select, input, button").attr("disabled", truefalse)
}

function toggleModal() {
    var myModal = bootstrap.Modal.getOrCreateInstance(document.getElementById('uploadFileModal'));
    myModal.toggle();
}

function showErrorMessage(msg) {
    alert(msg);
}

function togglePreviewModel() {
    var myModal = bootstrap.Modal.getOrCreateInstance(document.getElementById('previewModal'));
    myModal.toggle();
}