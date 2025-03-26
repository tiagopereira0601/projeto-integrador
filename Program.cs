using System;

class JogoDaVelha
{
    static string[,] tabuleiro = new string[3, 3];
    static string jogadorO = "O";  
    static string jogadorX = "X";  
    static string jogadorAtual = jogadorX;
    static int linha, coluna;
    static bool fimDeJogo = false;
    static Random random = new Random();

    static void Main()
    {
        int opcao;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== MENU ===");
            Console.WriteLine("1 - Jogar H x H");
            Console.WriteLine("2 - Jogar H x M (Fácil)");
            Console.Write("Escolha uma opção: ");
            opcao = Convert.ToInt32(Console.ReadLine());

            if (opcao == 1)
            {
                JogarHH();
                break;
            }
            else if (opcao == 2)
            {
                JogarHM();
                break;
            }
            else
            {
                Console.WriteLine("Opção inválida. Tente novamente.");
                Console.ReadKey();
            }
        }
    }

    static void InicializarTabuleiro()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                tabuleiro[i, j] = null;
            }
        }
    }

    static void ImprimirTabuleiro()
    {
        Console.Clear();
        for (int linhaTabuleiro = 0; linhaTabuleiro < 3; linhaTabuleiro++)
        {
            for (int colunaTabuleiro = 0; colunaTabuleiro < 3; colunaTabuleiro++)
            {
                if (tabuleiro[linhaTabuleiro, colunaTabuleiro] == null)
                    Console.Write("   ");
                else
                    Console.Write(" " + tabuleiro[linhaTabuleiro, colunaTabuleiro] + " ");
                if (colunaTabuleiro < 2)
                    Console.Write(" | ");
            }
            Console.WriteLine();
            if (linhaTabuleiro < 2)
                Console.WriteLine("----------------");
        }
    }

    static bool VerificarVitoria(string jogador)
    {
        
        return
            (tabuleiro[0, 0] == jogador && tabuleiro[0, 1] == jogador && tabuleiro[0, 2] == jogador) ||
            (tabuleiro[1, 0] == jogador && tabuleiro[1, 1] == jogador && tabuleiro[1, 2] == jogador) ||
            (tabuleiro[2, 0] == jogador && tabuleiro[2, 1] == jogador && tabuleiro[2, 2] == jogador) ||
            (tabuleiro[0, 0] == jogador && tabuleiro[1, 0] == jogador && tabuleiro[2, 0] == jogador) ||
            (tabuleiro[0, 1] == jogador && tabuleiro[1, 1] == jogador && tabuleiro[2, 1] == jogador) ||
            (tabuleiro[0, 2] == jogador && tabuleiro[1, 2] == jogador && tabuleiro[2, 2] == jogador) ||
            (tabuleiro[0, 0] == jogador && tabuleiro[1, 1] == jogador && tabuleiro[2, 2] == jogador) ||
            (tabuleiro[2, 0] == jogador && tabuleiro[1, 1] == jogador && tabuleiro[0, 2] == jogador);
    }

    static bool VerificarEmpate()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (tabuleiro[i, j] == null)
                    return false;
            }
        }
        return true;
    }

    static void JogarHH()
    {
        InicializarTabuleiro();
        while (!fimDeJogo)
        {
            ImprimirTabuleiro();
            Console.WriteLine("Jogador atual: " + jogadorAtual);
            Console.WriteLine("Digite a linha (1-3):");
            linha = Convert.ToInt32(Console.ReadLine()) - 1;

            Console.WriteLine("Digite a coluna (1-3):");
            coluna = Convert.ToInt32(Console.ReadLine()) - 1;

            if (tabuleiro[linha, coluna] == null)
            {
                tabuleiro[linha, coluna] = jogadorAtual;
                if (VerificarVitoria(jogadorAtual))
                {
                    ImprimirTabuleiro();
                    Console.WriteLine($"Vitória do jogador {jogadorAtual}!");
                    fimDeJogo = true;
                }
                else if (VerificarEmpate())
                {
                    ImprimirTabuleiro();
                    Console.WriteLine("Deu velha!");
                    fimDeJogo = true;
                }
                else
                {
                    jogadorAtual = (jogadorAtual == jogadorX) ? jogadorO : jogadorX;
                }
            }
            else
            {
                Console.WriteLine("Posição ocupada! Tente novamente.");
            }
        }
    }

    static void JogarHM()
    {
        InicializarTabuleiro();
        while (!fimDeJogo)
        {
            ImprimirTabuleiro();
            if (jogadorAtual == jogadorX)
            {
               
                Console.WriteLine("Jogador humano (X) faz a jogada.");
                Console.WriteLine("Digite a linha (1-3):");
                linha = Convert.ToInt32(Console.ReadLine()) - 1;

                Console.WriteLine("Digite a coluna (1-3):");
                coluna = Convert.ToInt32(Console.ReadLine()) - 1;

                if (tabuleiro[linha, coluna] == null)
                {
                    tabuleiro[linha, coluna] = jogadorAtual;
                    if (VerificarVitoria(jogadorAtual))
                    {
                        ImprimirTabuleiro();
                        Console.WriteLine("Vitória do jogador X!");
                        fimDeJogo = true;
                    }
                    else if (VerificarEmpate())
                    {
                        ImprimirTabuleiro();
                        Console.WriteLine("Deu velha!");
                        fimDeJogo = true;
                    }
                    else
                    {
                        jogadorAtual = jogadorO; 
                    }
                }
                else
                {
                    Console.WriteLine("Posição ocupada! Tente novamente.");
                }
            }
            else
            {
               
                Console.WriteLine("A IA (O) está fazendo a jogada...");
                JogadaIA();
                if (VerificarVitoria(jogadorAtual))
                {
                    ImprimirTabuleiro();
                    Console.WriteLine("A IA venceu!");
                    fimDeJogo = true;
                }
                else if (VerificarEmpate())
                {
                    ImprimirTabuleiro();
                    Console.WriteLine("Deu velha!");
                    fimDeJogo = true;
                }
                else
                {
                    jogadorAtual = jogadorX; 
                }
            }
        }
    }

    static void JogadaIA()
    {
       
        do
        {
            linha = random.Next(0, 3);
            coluna = random.Next(0, 3);
        } while (tabuleiro[linha, coluna] != null); 

        tabuleiro[linha, coluna] = jogadorO;
    }
}
