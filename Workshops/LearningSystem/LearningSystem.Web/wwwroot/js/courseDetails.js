let fileUpload = document.getElementById("file-upload");
fileUpload.addEventListener("input", updateValue);

function updateValue(e) {
    const fileUploadLabel = document.getElementById("file-upload-label");

    const parts = e.target.value.split("\\");
    const filename = parts[parts.length - 1];

    fileUploadLabel.textContent = filename;
};