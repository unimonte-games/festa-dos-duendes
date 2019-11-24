namespace Identificadores
{
    public enum JogadorID {J1 = 0, J2, J3, J4};

    public enum CenaID
    {
        Nenhum = -1,
        MenuInicial      = 0,
        Tabuleiro        = 1,
        QuebraBotao      = 2,
        BaldeDasMacas    = 3,
        PescaEscorrega   = 4,
        CogumeloQuente   = 5,
        FlautaHero       = 6,
        TelaCarregamento = 7,
        PreMiniJogo      = 8,
        Vencedor         = 9,
        // TODO: Colocar outros aqui
    };

    public enum TiposCasa { CasaBase, Conector, Moeda, BemMal, PowerUp, Garrafa, Acontecimento, MiniJogo };

    public enum Objetos { Cogumelo, Touca, Flauta, Flor, Pocao };

    public enum PowerUp
    {
        GincanaGratis, TrocaTudo, PoeiraNosOlhos, Teletransporte, Espanador, MaoEscorregadia, Emprestador,
        LadraoDeBanco, PilhaDeFolhas, PausaParaBanheiro, SuperEspanador, SuperEmprestador, SuperPilhaDeFolhas
    };
}
