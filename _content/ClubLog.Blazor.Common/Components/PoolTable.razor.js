export function focusNextCell(containerId) {
    const current = document.getElementById(containerId);
    if (!current) return;
    const table = current.closest('table');
    if (!table) return;
    const inputs = Array.from(table.querySelectorAll('.pool-score-cell input'));
    const currentInput = current.querySelector('input');
    const idx = inputs.indexOf(currentInput);
    if (idx >= 0 && idx < inputs.length - 1) {
        inputs[idx + 1].focus();
        inputs[idx + 1].select();
    }
}
