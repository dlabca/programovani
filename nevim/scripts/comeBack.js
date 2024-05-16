let docTitle = document.title;

window.addEventListener("blur", () => {
  document.title = "Vraťte se!!!";
});

window.addEventListener("focus", () => {
  document.title = docTitle;
});