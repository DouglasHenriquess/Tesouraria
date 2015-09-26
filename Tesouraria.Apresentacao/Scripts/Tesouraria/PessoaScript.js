$(function () {
    RegistraEventos();
});

function RegistraEventos() {
    onSalvar();
    onVoltar();
    onNovo();
    onPesquisar();
}

function onSalvar() {
    $("#btSalvar").click(function () {
        $("#form").submit();
    });
}

function onVoltar() {
    $("#btVoltar").click(function () {
        window.location.href = '/Pessoa/Index';
    });
}

function onNovo() {
    $("#btNovo").click(function () {
        window.location.href = "/Pessoa/Create";
    });
}

function onPesquisar() {
    $("#btPesquisar").click(function () {
        $("form").submit();
    });
}