$(function () {
    RegistraEventos();
});

function RegistraEventos() {
    onSalvar();
    onVoltar();
    onNovo();
    onPesquisar();
    onKeyPressValor();
}

function onSalvar() {
    $("#btSalvar").click(function () {
        $("#form").submit();
    });
}

function onVoltar() {
    $("#btVoltar").click(function () {
        window.location.href = "/Taxa/Index";
    });
}

function onNovo() {
    $("#btNovo").click(function () {
        window.location.href = "/Taxa/Create";
    });
}

function onPesquisar() {
    $("#btPesquisar").click(function () {
        $("form").submit();
    });
}