namespace fly;

public partial class MainPage : ContentPage
{
	int score=0;
	const int aberturaMinima = 50;
	const int forcaPulo=30;
	const int maxTempoPulando=3;//frames
	bool estaPulando=false;
	int tempoPulando=0;
	const int TempoEntreFrames = 25;
	const int gravidade = 6;
	double LarguraJanela;
	double AlturaJanela;
	int velocidade = 10;
	bool estaMorto = true;
	public MainPage()
	{
		InitializeComponent();
	}
	public async void Desenha()
	{
		while (!estaMorto)
		{
			AplicaGravidade();
			GerenciarCanos();
			await Task.Delay(TempoEntreFrames);
		}
	}
	void AplicaGravidade()
	{
		Passaro.TranslationY += gravidade;
		}

	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		LarguraJanela = w;
		AlturaJanela = h;

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
			imgCanoBaixo.TranslationY = imgCanoCima.TranslationY + aberturaMinima + imgCanoBaixo.HeightRequest;

			score++;
			LabelScore.Text = "Canos: " + score.ToString("D3");
		}
	}

 void AplicaPulo()
 {
	 Passaro.TranslationY-=forcaPulo;
	 tempoPulando++;
	 if (tempoPulando >= maxTempoPulando)
	 {
		estaPulando=false;
		tempoPulando=0;

	 }
 }
 void OnGridCliked(object sender, TappedEventArgs e)
 {
	estaPulando=true;
 }
 
    void Ei (object s,TappedEventArgs e )
{
	GameOverFrame.IsVisible = false;
	estaMorto = false;
	Inicializar ();
	Desenha ();
}
    void Inicializar()
	{
		Passaro.TranslationY = 0;
	}
	bool VerificaColisaoTeto()
	{
		var minY=AlturaJanela/2;
		if (Passaro.TranslationY <=minY)
		return true;
		else
		return false;
	}
	bool VerificaColisaoChao()
	{
		var maxY=AlturaJanela/2;
		if(Passaro.TranslationY >=maxY)
		return true;
		else 
		return false;
	}
	bool VerificaColisao()
	{
		if(! estaMorto)
		{
			if(VerificaColisaoTeto() ||
			VerificaColisaoChao())
			{
				return true;
			}
		}
		return true;
	}
}