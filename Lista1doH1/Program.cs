using System;

class JogoDaVelha
{
    static int[,] tabuleiro = new int[3, 3];
    static int jogador = 1;
    static int adversario = 2;

    static void Main(string[] args)
    {
        int[] melhorPosicao = MelhorJogadaMinMax(tabuleiro, jogador, adversario);
        Console.WriteLine($"Melhor posição: {melhorPosicao[0]}, {melhorPosicao[1]}");
    }

    static int Min(int[,] tabuleiro, int profundidade, int jogador, int adversario)
    {
        if (Vitoria(tabuleiro, jogador)) return 10 - profundidade;
        if (Vitoria(tabuleiro, adversario)) return profundidade - 10;
        if (Empate(tabuleiro)) return 0;

        int melhorValor = int.MaxValue;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (tabuleiro[i, j] == 0)  
                {
                    tabuleiro[i, j] = adversario;
                    int valor = Max(tabuleiro, profundidade + 1, jogador, adversario);
                    tabuleiro[i, j] = 0;
                    melhorValor = Math.Min(melhorValor, valor);
                }
            }
        }

        return melhorValor;
    }

    static int Max(int[,] tabuleiro, int profundidade, int jogador, int adversario)
    {
        if (Vitoria(tabuleiro, jogador)) return 10 - profundidade;
        if (Vitoria(tabuleiro, adversario)) return profundidade - 10;
        if (Empate(tabuleiro)) return 0;

        int melhorValor = int.MinValue;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (tabuleiro[i, j] == 0)
                {
                    tabuleiro[i, j] = jogador;
                    int valor = Min(tabuleiro, profundidade + 1, jogador, adversario);
                    tabuleiro[i, j] = 0;
                    melhorValor = Math.Max(melhorValor, valor);
                }
            }
        }

        return melhorValor;
    }

    static int[] MelhorJogadaMinMax(int[,] tabuleiro, int jogador, int adversario)
    {
        int melhorValor = int.MinValue;
        int[] melhorPosicao = new int[2];

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (tabuleiro[i, j] == 0) 
                {
                    tabuleiro[i, j] = jogador;
                    int valor = Min(tabuleiro, 0, jogador, adversario);
                    tabuleiro[i, j] = 0;

                    if (valor > melhorValor)
                    {
                        melhorValor = valor;
                        melhorPosicao[0] = i;
                        melhorPosicao[1] = j;
                    }
                }
            }
        }

        return melhorPosicao;
    }

    static bool Vitoria(int[,] tabuleiro, int jogador)
    {

        for (int i = 0; i < 3; i++)
        {
            if (tabuleiro[i, 0] == jogador && tabuleiro[i, 1] == jogador && tabuleiro[i, 2] == jogador)
                return true;
            if (tabuleiro[0, i] == jogador && tabuleiro[1, i] == jogador && tabuleiro[2, i] == jogador)
                return true;
        }

        if (tabuleiro[0, 0] == jogador && tabuleiro[1, 1] == jogador && tabuleiro[2, 2] == jogador)
            return true;
        if (tabuleiro[0, 2] == jogador && tabuleiro[1, 1] == jogador && tabuleiro[2, 0] == jogador)
            return true;

        return false;
    }

    static bool Empate(int[,] tabuleiro)
    {
        foreach (int cell in tabuleiro)
        {
            if (cell == 0) return false;
        }
        return true;
    }
}
