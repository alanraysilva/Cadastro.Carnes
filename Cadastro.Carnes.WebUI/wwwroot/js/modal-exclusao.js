let idParaExcluir = null;
let acaoExcluir = null;

function abrirModalExcluir(id, descricao, callback) {
    idParaExcluir = id;
    acaoExcluir = callback || null;

    document.getElementById('descricaoExclusao').textContent = descricao;

    // Remove listeners antigos antes de adicionar novo
    const btn = document.getElementById('btnConfirmarExclusao');
    const newBtn = btn.cloneNode(true);
    btn.parentNode.replaceChild(newBtn, btn);

    newBtn.addEventListener('click', function () {
        if (typeof acaoExcluir === 'function') {
            acaoExcluir(idParaExcluir);
        }
        // Fecha o modal depois
        const modal = bootstrap.Modal.getInstance(document.getElementById('modalConfirmarExclusao'));
        if (modal) modal.hide();
    });

    new bootstrap.Modal(document.getElementById('modalConfirmarExclusao')).show();
}