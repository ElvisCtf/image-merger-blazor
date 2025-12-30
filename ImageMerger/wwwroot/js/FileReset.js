window.fileReset = {
    clear: function (elementId) {
        const input = document.getElementById(elementId);
        if (input) {
            input.value = "";
        }
    }
};