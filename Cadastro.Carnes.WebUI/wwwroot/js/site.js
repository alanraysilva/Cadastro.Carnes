// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let idExclusaoAtual = null;
let callbackExclusao = null;

function abrirModalExcluirGenerico(id, descricao, callback) {
    idExclusaoAtual = id;
    callbackExclusao = callback;

    document.getElementById("descricaoExclusao").textContent = descricao;
    const modal = new bootstrap.Modal(document.getElementById("modalConfirmarExclusao"));
    modal.show();
}

document.getElementById("btnConfirmarExclusao").addEventListener("click", function () {
    if (typeof callbackExclusao === 'function') {
        callbackExclusao(idExclusaoAtual);
    }
    const modalElement = bootstrap.Modal.getInstance(document.getElementById("modalConfirmarExclusao"));
    modalElement.hide();
});

function formatarDocumento(doc) {
    if (!doc) return '';
    doc = doc.replace(/\D/g, '');
    if (doc.length === 11)
        return doc.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4'); // CPF
    if (doc.length === 14)
        return doc.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, '$1.$2.$3/$4-$5'); // CNPJ
    return doc;
}