namespace fly;

public partial class MainPage : ContentPage
{
	int score = 0;
	const int aberturaMinima = 50;
	const int forcaPulo = 30;
	const int maxTempoPulando = 3;//frames
	bool estaPulando = false;
	int tempoPulando = 0;
	const int TempoEntreFrames = 25;
	const int gravidade = 6;
	double LarguraJanela;
	double AlturaJanela;
	int velocidade = 10;
	bool estaMorto = true;
	const int tamanhoMinimoPassagem = 200;

	public MainPage()
	{
		InitializeComponent();
	}

	public async void Desenha()
	{
		while (!estaMorto)
		{
			if (estaPulando)
				AplicaPulo();
			else
				AplicaGravidade();

			GerenciarCanos();

			if (VerificaColisao())
			{
				estaMorto = true;
				LabelScore.Text = "Você passou por\n" + score + " canos";
				GameOverFrame.IsVisible = true;
				break;
			}

			await Task.Delay(TempoEntreFrames);
		}
	}
	void AplicaGravidade()
	{
		Passaro.TranslationY += gravidade;
	}

	protected override void OnSizeAllocated(double width, double height)
	{
		base.OnSizeAllocated(width, height);

		LarguraJanela = width;
		AlturaJanela = height;
		if (height > 0)
		{
			imgCanoCima.HeightRequest = height - imgChao.HeightRequest;
			imgCanoBaixo.HeightRequest = height - imgChao.HeightRequest;
		}
	}

	void GerenciarCanos()
	{

		imgCanoCima.TranslationX -= velocidade;
		imgCanoBaixo.TranslationX -= velocidade;
		if (imgCanoBaixo.TranslationX < -LarguraJanela)
		{
			imgCanoBaixo.TranslationX = 20;
			imgCanoCima.TranslationX = 20;

			var alturaMaxima = -50;
			var alturaMinima = -imgCanoBaixo.HeightRequest;

			imgCanoCima.TranslationY = Random.Shared.Next((int)alturaMinima, (int)alturaMaxima);
			imgCanoBaixo.TranslationY = imgCanoCima.TranslationY + tamanhoMinimoPassagem + imgCanoBaixo.HeightRequest;

			score++;
			LabelScore.Text = "Score: " + score.ToString("D3");
			if (score % 4 == 0)
				velocidade++;
		}
	}

	void AplicaPulo()
	{
		Passaro.TranslationY -= forcaPulo;
		tempoPulando++;
		if (tempoPulando >= maxTempoPulando)
		{
			estaPulando = false;
			tempoPulando = 0;

		}
	}
	void OnGridCliked(object sender, TappedEventArgs e)
	{
		estaPulando = true;
	}

	void Ei(object s, TappedEventArgs e)
	{
		GameOverFrame.IsVisible = false;
		estaMorto = false;
		Inicializar();
		Desenha();
	}
	void Inicializar()
	{
		Passaro.TranslationY = 0;
	}

	bool VerificaColisaoCanoCima()
	{
		var posicaoHorizontalPardal = (LarguraJanela - 50) - (Passaro.WidthRequest / 2);
		var posicaoVerticalPardal = (AlturaJanela / 2) - (Passaro.HeightRequest / 2) + Passaro.TranslationY;

		if (
			 posicaoHorizontalPardal >= Math.Abs(imgCanoCima.TranslationX) - imgCanoCima.WidthRequest &&
			 posicaoHorizontalPardal <= Math.Abs(imgCanoCima.TranslationX) + imgCanoCima.WidthRequest &&
			 posicaoVerticalPardal <= imgCanoCima.HeightRequest + imgCanoCima.TranslationY
		   )
			return true;
		else
			return false;
	}

	bool VerificaColisaoCanoBaixo()
	{
		var posicaoHorizontalPardal = LarguraJanela - 50 - Passaro.WidthRequest / 2;
		var posicaoVerticalPardal = (AlturaJanela / 2) + (Passaro.HeightRequest / 2) + Passaro.TranslationY;

		var yMaxCano = imgCanoCima.HeightRequest + imgCanoCima.TranslationY + aberturaMinima;

		if (
			 posicaoHorizontalPardal >= Math.Abs(imgCanoCima.TranslationX) - imgCanoCima.WidthRequest &&
			 posicaoHorizontalPardal <= Math.Abs(imgCanoCima.TranslationX) + imgCanoCima.WidthRequest &&
			 posicaoVerticalPardal >= yMaxCano
		   )
			return true;
		else
			return false;
	}
	bool VerificaColisaoTeto()
	{
		var alturaDoTeto = AlturaJanela / 2;
		if (Passaro.TranslationY <= -alturaDoTeto)
			return true;
		else
			return false;
	}
	bool VerificaColisaoChao()
	{
		var alturaDoTeto = AlturaJanela / 2;
		if (Passaro.TranslationY >= alturaDoTeto - imgChao.Height)
			return true;
		else
			return false;
	}
	 bool VerificaColisao()
  {
    return (!estaMorto && (VerificaColisaoChao() || VerificaColisaoTeto() || VerificaColisaoCanoBaixo() || VerificaColisaoCanoCima()));
  }

}