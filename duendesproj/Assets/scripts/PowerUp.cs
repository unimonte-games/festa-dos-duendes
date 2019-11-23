namespace Identificadores
{
    public enum JogadorID {J1 = 0, J2, J3, J4};

    public enum CenaID
    {
        Nenhum = -1,
        Tabuleiro        = 0,
        QuebraBotao      = 1,
        BaldeDasMacas    = 2,
        PescaEscorrega   = 3,
        CogumeloQuente   = 4,
        FlautaHero       = 5,
        TelaCarregamento = 6,
        MenuInicial      = 7,
        PreMiniJogo      = 8,
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