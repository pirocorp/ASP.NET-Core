function onReplyClick(parentId) {

    document.getElementById("parent-id").value = parentId;

    let elementId = "add-reply-form";
    let element = document.getElementById(elementId);
    element.style = "";

    document.getElementById(elementId).scrollIntoView({ behavior: "smooth" });
}