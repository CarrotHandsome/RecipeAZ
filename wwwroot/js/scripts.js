window.getCursorPosition = (id) => {
    const input = document.getElementById(id);
    const start = input.selectionStart;
    const end = input.selectionEnd;

    return { item1: start, item2:end };
};