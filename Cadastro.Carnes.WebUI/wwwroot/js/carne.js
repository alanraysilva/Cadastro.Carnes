let carneSelecionadaId = 0;

function abrirModalCriar() {
    carneSelecionadaId = 0;
    $('#descricao').val('');
    $('#origemId').val('');
    $('#modalCarne').modal('show');
}

function abrirModalEditar(id, descricao, origemId) {
    carneSelecionadaId = id;
    $('#descricao').val(descricao);
    $('#origemId').val(origemId);
    $('#modalCarne').modal('show');
}

function abrirModalExcluir(id, descricao) {
    carneSelecionadaId = id;
    $('#descricaoExclusao').text(descricao);
    $('#modalExcluirCarne').modal('show');
}

$('#formCarne').submit(function (e) {
    e.preventDefault();
    const data = {
        id: carneSelecionadaId,
        descricao: $('#descricao').val(),
        origemId: $('#origemId').val()
    };

    const url = carneSelecionadaId === 0 ? '/Carne/Create' : `/Carne/Edit/${carneSelecionadaId}`;
    $.ajax({
        url,
        type: 'POST',
        data,
        success: () => location.reload()
    });
});

$('#btnConfirmarExclusao').click(function () {
    $.ajax({
        url: `/Carne/Delete/${carneSelecionadaId}`,
        type: 'POST',
        success: () => location.reload()
    });
});
