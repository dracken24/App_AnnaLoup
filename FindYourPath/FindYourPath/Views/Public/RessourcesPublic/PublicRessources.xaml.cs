using FindYourPath.Properties;
using FindYourPath.Views.Private.Profile;
using FindYourPath.Views.Public.RessourcesPublic;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindYourPath.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PublicRessources : ContentPage
	{
		public PublicRessources()
		{
			InitializeComponent();

			Title = "Ressources Publique";
		}

		private Dictionary<string, List<ListeRessources>> cityResources = new Dictionary<string, List<ListeRessources>>
		{
			{
				"Quebec", new List<ListeRessources>
				{
					new ListeRessources
					{
						City = "Quebec",
						Name = "Aide Allimentaire",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"La Bouchée Généreuse", new OneRessource
								{
									Nom = "La Bouchée Généreuse",
									Adresse = "145 Boulevard Wilfrid-Hamel, Québec, QC G1L 4H8",
									Telephone = "418 648-8588",
									URL = "https://laboucheegenereuse.org/",
									Description = "Services de première ligne en contribuant à l’amélioration du milieu de vie et de l’alimentation de la population démunie de la région de Québec."
								}
							},
							{
								"Solidarité Familles", new OneRessource
								{
									Nom = "Solidarité Familles",
									Adresse = "5720, boulevard de l’Ormière, Québec (Québec), G1P 1K7",
									Telephone = "581 741-1919",
									URL = "https://solidaritefamilles.ca/",
									Description = "Nos différents ateliers de cuisine permettent de se sécuriser au point de vue alimentaire et de manger sainement. De plus, ils permettent de rencontrer des gens, tout en apprenant à se familiariser avec la cuisine. La contribution de Moisson Québec permet notamment d’offrir un panier de denrées alimentaires aux participants de la cuisine créative et du tri-transformation, selon le nombre de portions auxquelles ils ont droit.\r\nNous animons chaque mois plusieurs groupes de cuisine composés de citoyens de Duberger et des Saules."
								}
							},
							{
								"Entraide Agapè", new OneRessource
								{
									Nom = "Entraide Agapè",
									Adresse = "3148, chemin Royal Québec, G1E 1V2",
									Telephone = "418-661-7485",
									URL = "https://entraideagape.org/",
									Description = "Situés à Beauport, nous offrons une aide alimentaire, matérielle et sociale aux personnes et aux familles en situation de pauvreté ou de vulnérabilité de Québec. Nous contribuons également à l’économie circulaire du Québec en donnant une nouvelle vie aux ressources grâce aux activités de la banque alimentaire et de la ressourcerie (friperie)."
								}
							},
						},
					},
					new ListeRessources
					{
						City = "Quebec",
						Name = "Aide Financiere",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"A venir", new OneRessource
								{
									Nom = "A venir",
									Adresse = "-",
									Telephone = "-",
									URL = "-",
									Description = "-"
								}
							},
						},
					},
					new ListeRessources
					{
						City = "Quebec",
						Name = "Aide Psycho Sociale",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"Liste de ressources par région", new OneRessource
								{
									Nom = "Liste de ressources par région",
									Adresse = "-",
									Telephone = "-",
									URL = "https://www.psycho-ressources.com/",
									Description = "-"
								}
							},
							{
								"Le Centre Communautaire l’Amitié", new OneRessource
								{
									Nom = "Le Centre Communautaire l’Amitié",
									Adresse = "59 Rue Notre-Dame-des-Anges, Québec, QC G1K 3E4",
									Telephone = "418 522-5719",
									URL = "https://www.centrecommunautairelamitie.com/",
									Description = "Le Centre Communautaire l’Amitié (CCA) est un milieu de vie pour les adultes vivant des difficultés psychologiques et/ou socio-économiques. Son approche est basée sur l’accueil et le respect des personnes, la valorisation de leur potentiel et leur implication dans la vie du Centre et dans la communauté. L’équipe d’intervenants sociaux offre de l’écoute, du soutien, de la relation d’aide et de l’accompagnement au niveau des démarches personnelles."
								}
							},
							{
								"AGIR", new OneRessource
								{
									Nom = "AGIR",
									Adresse = "160, rue St-Joseph EstQuébec (Québec), G1K 6E7",
									Telephone = "418-640-5253",
									URL = "https://www.agirensantementale.ca",
									Description = "Voici le contenu de la mission de l'AGIR que l'on peut retrouver dans sa  charte:\r\n* Regrouper les organismes communautaires en santé mentale de la région de Québec;\r\n* Échanger des services, du support et de l’information entre les groupes;\r\n* Promouvoir la création et le maintien de ressources suffisantes pour la population;\r\n* Promouvoir les droits et intérêts des personnes qui vivent des problèmes émotionnels et psychologiques et leur donner toute l’information au sujet de leurs droits; \r\n* Promouvoir une compréhension globale de la santé mentale qui tient compte des conditions sociales, économiques, politiques, familiales, culturelles, psychologiques et physiologiques;\r\n* Revendiquer et obtenir des fonds, subventions et autres contributions adéquates et diversifiées pour réaliser les objectifs du «Regroupement», tout en préservant l’autonomie financière des groupes membres;\r\n* Éduquer et sensibiliser la population aux différents objectifs du «Regroupement»;\r\n* Développer des contacts avec d’autres regroupements et organismes existants."
								}
							},
							{
								"Océan", new OneRessource
								{
									Nom = "Océan",
									Adresse = "2120, rue Boivin, local 205 Québec (Québec) G1V 1N7",
									Telephone = "418 522-3283",
									URL = "https://org-ocean.com/",
									Description = "Depuis 1991, OCÉAN œuvre de façon significative dans le domaine de la santé mentale dans la région de la Capitale-Nationale.\r\nLa mission de cet organisme à but non lucratif est de favoriser l'autonomie fonctionnelle et sociale des personnes ayant un problème de santé mentale ou de désorganisation de vie et de contribuer à la promotion de la santé mentale ainsi qu'à la démystification des maladies mentales.\r\n"
								}
							},
							{
								"Réseau Avant de craquer", new OneRessource
								{
									Nom = "Réseau Avant de craquer",
									Adresse = "219-1990, rue Cyrille-Duquet Québec (Québec) G1N 4K8",
									Telephone = "1 855 272-7837",
									URL = "https://www.avantdecraquer.com/",
									Description = "Que vous soyez un parent, un enfant, un frère, une sœur, un conjoint, un ami, un collègue, le Réseau Avant de craquer est là pour vous aider à mieux composer avec un proche vivant avec un problème de santé mentale.\r\nQue ce soit en lien avec l’un ou l’autre de ces troubles mentaux (troubles psychotiques comme la schizophrénie, psychose, dépression, troubles anxieux, trouble bipolaire, trouble obsessionnel-compulsif (TOC), troubles de la personnalité, trouble de la personnalité limite (TPL), etc.), vous trouverez ici des réponses face à des situations qui vous dépassent avec la santé mentale de vos proches. Trouvez des ressources utiles dans notre coffre à outils, notre banque de balados et notre galerie de vidéos.\r\nNaviguez à travers nos différents portails pour trouver des réponses aux questions fréquemment posées ainsi que des trucs et astuces pour mieux-vivre votre quotidien. Notre plateforme web est là pour vous accompagner tant au travers des étapes importantes de changement, qu’au travers des défis de tous les jours.\r\n"
								}
							},
							{
								"Centre de prévention du suicide", new OneRessource
								{
									Nom = "Centre de prévention du suicide",
									Adresse = "1310 1re Avenue Québec (Québec), Canada G1L 3L1",
									Telephone = "1 866 277-3553",
									URL = "https://www.cpsquebec.ca/",
									Description = "Nous offrons, dans la grande région de Québec, un ensemble de services professionnels et spécialisés visant la prévention, l’intervention et la postvention auprès de personnes suicidaires, de leurs proches et des personnes endeuillées.\r\n"
								}
							},
						},
					},
					new ListeRessources
					{
						City = "Quebec",
						Name = "Hebergement",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"A venir", new OneRessource
								{
									Nom = "A venir",
									Adresse = "-",
									Telephone = "-",
									URL = "-",
									Description = "-"
								}
							},
						},
					},
					new ListeRessources
					{
						City = "Quebec",
						Name = "Emploies/Etudes",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"Emploi Québec", new OneRessource
								{
									Nom = "Emploi Québec",
									Adresse = "425 Rue Jacques-Parizeau, Québec, QC G1R 4Z1",
									Telephone = "1 877 767-8773",
									URL = "https://www.emploiquebec.gouv.qc.ca/",
									Description = "Trouver un emploi, métiers et professions, programmes d'embauche, information sur le marché du travail."
								}
							},
							{
								"Option travail", new OneRessource
								{
									Nom = "Option travail",
									Adresse = "2750, chemin Sainte-Foy, bureau 295 (porte 2) Québec (Québec)  G1V 1V6",
									Telephone = "418 651-6415",
									URL = "https://optiontravail.com/",
									Description = "L’équipe d’Option-travail souhaite faire la différence dans votre cheminement de carrière et votre épanouissement professionnel ou dans l’essor de votre organisation. Son objectif est de créer un impact positif sur l’avenir socioprofessionnel des personnes."
								}
							},
							{
								"GIT, Service conseil à l'emploi", new OneRessource
								{
									Nom = "GIT, Service conseil à l'emploi",
									Adresse = "245, rue Soumande, bureau 280 Québec (Québec) G1M 3H6",
									Telephone = "418 686-1888",
									URL = "https://git.qc.ca/",
									Description = "Chez GIT, obtenez tout ce dont vous avez besoin pour décrocher rapidement un emploi qui répond à vos besoins, grâce à une recherche d’emploi efficace.\r\nNos services comprennent (entre autres) :\r\nRédaction de CV\r\nRédaction de lettre de présentation\r\nPréparation d’une entrevue d’embauche\r\nStratégies de recherche d’emploi\r\nExploration des possibilités d’emploi\r\nPour les personnes immigrantes : décrocher un emploi à Québec\r\nPour les personnes retraitées : harmoniser retraite et travail\r\n"
								}
							},
							{
								"APE", new OneRessource
								{
									Nom = "APE",
									Adresse = "710 Rue Bouvier Suite 275, Québec, QC G2J 1C2",
									Telephone = "418 628-6389",
									URL = "https://ape.qc.ca/",
									Description = "Forte de plus de 35 ans d’expérience dans le domaine de l’employabilité, l’APE peut vous accompagner que vous soyez un individu à la recherche d’un emploi ou une entreprise ayant besoin de services-conseils en ressources humaines."
								}
							},
						},
					},

					new ListeRessources
					{
						City = "Quebec",
						Name = "Therapies",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"CTBQ", new OneRessource
								{
									Nom = "CTBQ",
									Adresse = "Av. Madeleine de Verchère 889, Quebec, G1S 4K6",
									Telephone = "418-262-6428",
									URL = "https://www.ctbq.ca/",
									Description = "Anxiété, stress, deuil, séparation, difficultés relationnelles, dépression, phobies, obsessions, etc…\r\nVous cherchez une approche dynamique, concrète pour résoudre les problèmes que vous rencontrez?\r\nLa thérapie stratégique brève peut répondre à vos attentes en vous donnant de nouveaux outils pour gérer vos difficultés …\r\n"
								}
							},
							{
								"Thérapie Québec", new OneRessource
								{
									Nom = "Thérapie Québec",
									Adresse = "3181, Ch Ste-Foy, Bureaux 260 et 280 Québec, QC G1X 1R3",
									Telephone = "418-204-8284",
									URL = "https://therapiequebecandc.ca/",
									Description = "Difficultés relationnelles avec soi-même, un proche ou votre entourage. Infériorité, jalousie, rivalité, possessivité, insécurité, difficulté à prendre sa place, à s’affirmer, à dire « non », difficulté à faire confiance, impression de tourner en rond).\r\nProblèmes de communication avec un enfant, un membre de sa famille, un collègue, un patron, un conjoint, un ex-conjoint, un voisin. Lorsque vous avez l’impression que vous ne parlez pas la même langue.\r\nDeuil, séparation, crise conjugale, conflit au travail, perte d’emploi, interruption de grossesse, choix difficile.\r\nBesoin d’aide et d’accompagnement pour sortir de l’isolement, retrouver plus d’amour de soi et reprendre sa vie en main (sortie du chaos émotionnel, mieux se comprendre, confiance en soi, passage à l’action, estime de soi, développement personnel)\r\n"
								}
							},
							{
								"Clinique de Psychothérapie Brève", new OneRessource
								{
									Nom = "Clinique de Psychothérapie Brève",
									Adresse = "2480 Chemin Sainte-Foy, bureau 180, Quebec (Québec) G1V 1T6, Canada",
									Telephone = "418 653-2920",
									URL = "https://cliniquedepsychotherapiebreve.com/",
									Description = "Conseiller et outiller les personnes souffrant de diverses problématiques psychologiques, notamment: la dépression, l'anxiété, la dépendance, l'insomnie, les troubles de la personnalité, le trouble déficitaire de l'attention avec ou sans hyperactivité, la faible estime de soi ainsi que les problèmes relationnels. \r\n"
								}
							},
							{
								"Maison d'entraide l'Arc-en-Ciel", new OneRessource
								{
									Nom = "Maison d'entraide l'Arc-en-Ciel",
									Adresse = "346 rue du Parvis C.P.30010 Québec, Québec G1K 8Y1",
									Telephone = "418 522-2915",
									URL = "http://www.maison-arc-en-ciel.org/",
									Description = "La maison d'entraide l'Arc-en-Ciel est une ressource qui vient en aide à une clientèle aux prises avec une dépendance avec l'alcool ou les drogues. Elle accueille des hommes âgés de 18 ans et plus et les soutient dans leurs efforts.\r\nLa thérapie est basée sur le mode de vie des 12 étapes des alcooliques anonymes. La maison offre un séjour de 30 jours en thérapie interne, à l'intérieur d'un petit groupe de 10 à 12 personnes, dans une atmosphère chaleureuse, avec une excellente cuisine maison.\r\n"
								}
							},
							{
								"Portage", new OneRessource
								{
									Nom = "Portage",
									Adresse = "150, rue Saint-Joseph Est, Québec (Québec) G1K 3A7",
									Telephone = "418 524-0320",
									URL = "https://portage.ca/fr/",
									Description = "Chez Portage, tu seras entouré d’une communauté de personnes qui partagent le même objectif que toi, et vous vous aiderez mutuellement à l’atteindre.\r\nPersonne ne sait exactement ce que c’est que d’être toi, mais un grand nombre de personnes ont fait face à des difficultés semblables et ont réussi à reprendre leur vie en main.\r\nTu peux vaincre tes problèmes de toxicomanie. Du temps, de la patience et beaucoup d’efforts sont nécessaires, mais si tu es déterminé, tu peux réussir! Toi seul tu peux le faire, mais tu ne peux le faire seul.\r\n"
								}
							},
							{
								"Le Portail", new OneRessource
								{
									Nom = "Le Portail",
									Adresse = "1240, Rte de Fossambault Nord St-Augustin-de-Desmaures Québec CA G3A 1W8",
									Telephone = "418 878-2867",
									URL = "https://therapieleportail.org/",
									Description = "Le Portail est un organisme à but non lucratif fondé en 1994. Notre mission est de venir en aide aux femmes adultes qui vivent des difficultés reliées à la consommation d’alcool, de drogues et de médicaments. À cette fin, nous leur offrons une thérapie intensive de 28 jours avec hébergement. Nous les aidons à retrouver leur dignité, à redonner un sens à leur vie, à développer leur autonomie, à assumer leurs responsabilités et à améliorer leur qualité de vie.​Le Portail est certifié par le Centre intégré universitaire de santé et de services sociaux (CIUSSS), ce qui témoigne de la grande qualité de nos services à tous les niveaux et de notre volonté de nous améliorer constamment."
								}
							},
							{
								"Le Passage", new OneRessource
								{
									Nom = "Le Passage",
									Adresse = "2860 Chem. des Quatre-Bourgeois #210, Québec City, Quebec G1V 1Y3",
									Telephone = "418 527-0916",
									URL = "https://centrelepassage.org/",
									Description = "Le centre Le Passage vient en aide aux proches des personnes vivant une ou des dépendance(s) (alcool, drogues, médicaments, jeu excessif, monde virtuel ou toute autre forme de dépendance), ainsi qu’à l’entourage des personnes judiciarisées. Nous offrons également des services aux personnes qui vivent des relations de dépendance affective et/ou de codépendance, ainsi qu’à toutes personnes qui veulent approfondir leur rapport affectif et relationnel aux dépendances. \r\nNos services s’adressent aux parents, aux enfants, aux conjoints, aux frères, aux sœurs, aux amis, aux voisins, aux collègues de travail et aux autres proches qui se questionnent sur l’attitude à adopter ou les changements à apporter pour améliorer leur qualité de vie et pour mieux comprendre la personne qui vit la dépendance.\r\n"
								}
							},
							{
								"CASA", new OneRessource
								{
									Nom = "CASA",
									Adresse = "-",
									Telephone = "418 871-8380",
									URL = "https://www.centrecasa.qc.ca/",
									Description = "Des solutions sont possibles. Le Centre CASA propose un programme de traitement pour cesser ou réduire les méfaits de la consommation d’alcool, de drogues et de médicaments. Le Centre CASA offre, en hébergement de 28 jours, une thérapie fermée qui tient compte des meilleures pratiques en matière de traitement des dépendances. L’accompagnement est fait par une équipe de professionnels et vise à apporter un équilibre de vie et une plus grande autonomie.\r\nLa mission du Centre se réalise dans cinq principaux champs d’expertise :\r\nAlcoolisme et toxicomanie\r\nJeux pathologique et cyberdépendance\r\nDépendances chez les personnes portant où ayant porté l’uniforme\r\nGestion des dépendances dans le milieu de travail\r\nFormation et recherche"
								}
							},
							{
								"AA Québec", new OneRessource
								{
									Nom = "AA Québec",
									Adresse = "-",
									Telephone = "418 529-0015",
									URL = "https://aa-quebec.org/aaqc_wp/",
									Description = "INFORMATIONS SUR LES AA Les Alcooliques anonymes sont un mouvement international d’hommes et de femmes qui avaient un problème d’alcool. Non professionnels, ils s’autofinancent, ils sont multiraciaux, apolitiques et on les retrouve presque partout. Il n’y a aucune limite d’âge ni"
								}
							},
						},
					},
					new ListeRessources
					{
						City = "Quebec",
						Name = "Fripperies",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"Café rencontre", new OneRessource
								{
									Nom = "Café rencontre",
									Adresse = "796 rue St-Joseph EST, CP30146 Québec, Québec, G1K 3C3",
									Telephone = "418 266-0261",
									URL = "https://www.caferencontre.org/",
									Description = "La friperie du Café Rencontre a pignon sur rue au 798, rue Saint-Joseph. Ce service représente un atout pour le Café rencontre du centre-ville, car il nous permet de fournir une aide importante aux bénéficiaires de notre soupe populaire. De plus, en cas de besoin, nous pouvons offrir gratuitement ou à coût symbolique les vêtements dont ils ont besoin.\r\nGrâce à l’excellent travail de la gérante et de son équipe, la friperie offre des vêtements de qualités et le profit réalisé permet de financer les activités de la soupe populaire. Les ventes de la friperie contribuent à remplir les assiettes de la soupe populaire. \r\n"
								}
							},
						},
					},
				}
			},
			{
				"Montreal", new List<ListeRessources>
				{
					new ListeRessources
					{
						City = "Montreal",
						Name = "Aide Allimentaire",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"A venir", new OneRessource
								{
									Nom = "A venir",
									Adresse = "-",
									Telephone = "-",
									URL = "-",
									Description = "-"
								}
							},
						},
					},
					new ListeRessources
					{
						City = "Montreal",
						Name = "Aide Financiere",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"A venir", new OneRessource
								{
									Nom = "A venir",
									Adresse = "-",
									Telephone = "-",
									URL = "-",
									Description = "-"
								}
							},
						},
					},
					new ListeRessources
					{
						City = "Montreal",
						Name = "Aide Psycho Sociale",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"A venir", new OneRessource
								{
									Nom = "A venir",
									Adresse = "-",
									Telephone = "-",
									URL = "-",
									Description = "-"
								}
							},
						},
					},
					new ListeRessources
					{
						City = "Montreal",
						Name = "Hebergement",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"A venir", new OneRessource
								{
									Nom = "A venir",
									Adresse = "-",
									Telephone = "-",
									URL = "-",
									Description = "-"
								}
							},
						},
					},
					new ListeRessources
					{
						City = "Montreal",
						Name = "Emploies/Etudes",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"A venir", new OneRessource
								{
									Nom = "A venir",
									Adresse = "-",
									Telephone = "-",
									URL = "-",
									Description = "-"
								}
							},
						},
					},
					new ListeRessources
					{
						City = "Montreal",
						Name = "Therapies",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"A venir", new OneRessource
								{
									Nom = "A venir",
									Adresse = "-",
									Telephone = "-",
									URL = "-",
									Description = "-"
								}
							},
						},
					},
					new ListeRessources
					{
						City = "Montreal",
						Name = "Fripperies",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"A venir", new OneRessource
								{
									Nom = "A venir",
									Adresse = "-",
									Telephone = "-",
									URL = "-",
									Description = "-"
								}
							},
						},
					},
				}
			},
			{
				"Outaouais", new List<ListeRessources>
				{
					new ListeRessources
					{
						City = "Outaouais",
						Name = "Aide Allimentaire",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"A venir", new OneRessource
								{
									Nom = "A venir",
									Adresse = "-",
									Telephone = "-",
									URL = "-",
									Description = "-"
								}
							},
						},
					},
					new ListeRessources
					{
						City = "Outaouais",
						Name = "Aide Financiere",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"A venir", new OneRessource
								{
									Nom = "A venir",
									Adresse = "-",
									Telephone = "-",
									URL = "-",
									Description = "-"
								}
							},
						},
					},
					new ListeRessources
					{
						City = "Outaouais",
						Name = "Aide Psycho Sociale",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"A venir", new OneRessource
								{
									Nom = "A venir",
									Adresse = "-",
									Telephone = "-",
									URL = "-",
									Description = "-"
								}
							},
						},
					},
					new ListeRessources
					{
						City = "Outaouais",
						Name = "Hebergement",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"A venir", new OneRessource
								{
									Nom = "A venir",
									Adresse = "-",
									Telephone = "-",
									URL = "-",
									Description = "-"
								}
							},
						},
					},
					new ListeRessources
					{
						City = "Outaouais",
						Name = "Emploies/Etudes",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"A venir", new OneRessource
								{
									Nom = "A venir",
									Adresse = "-",
									Telephone = "-",
									URL = "-",
									Description = "-"
								}
							},
						},
					},
					new ListeRessources
					{
						City = "Outaouais",
						Name = "Therapies",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"A venir", new OneRessource
								{
									Nom = "A venir",
									Adresse = "-",
									Telephone = "-",
									URL = "-",
									Description = "-"
								}
							},
						},
					},
					new ListeRessources
					{
						City = "Outaouais",
						Name = "Fripperies",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"A venir", new OneRessource
								{
									Nom = "A venir",
									Adresse = "-",
									Telephone = "-",
									URL = "-",
									Description = "-"
								}
							},
						},
					},
				}
			},
			{
				"Other", new List<ListeRessources>
				{
					new ListeRessources
					{
						City = "Other",
						Name = "Aide Allimentaire",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"A venir", new OneRessource
								{
									Nom = "A venir",
									Adresse = "-",
									Telephone = "-",
									URL = "-",
									Description = "-"
								}
							},
						},
					},
					new ListeRessources
					{
						City = "Other",
						Name = "Aide Financiere",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"A venir", new OneRessource
								{
									Nom = "A venir",
									Adresse = "-",
									Telephone = "-",
									URL = "-",
									Description = "-"
								}
							},
						},
					},
					new ListeRessources
					{
						City = "Other",
						Name = "Aide Psycho Sociale",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"A venir", new OneRessource
								{
									Nom = "A venir",
									Adresse = "-",
									Telephone = "-",
									URL = "-",
									Description = "-"
								}
							},
						},
					},
					new ListeRessources
					{
						City = "Other",
						Name = "Hebergement",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"A venir", new OneRessource
								{
									Nom = "A venir",
									Adresse = "-",
									Telephone = "-",
									URL = "-",
									Description = "-"
								}
							},
						},
					},
					new ListeRessources
					{
						City = "Other",
						Name = "Emploies/Etudes",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"A venir", new OneRessource
								{
									Nom = "A venir",
									Adresse = "-",
									Telephone = "-",
									URL = "-",
									Description = "-"
								}
							},
						},
					},
					new ListeRessources
					{
						City = "Other",
						Name = "Therapies",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"A venir", new OneRessource
								{
									Nom = "A venir",
									Adresse = "-",
									Telephone = "-",
									URL = "-",
									Description = "-"
								}
							},
						},
					},
					new ListeRessources
					{
						City = "Other",
						Name = "Fripperies",
						Options = new Dictionary<string, OneRessource>
						{
							{
								"A venir", new OneRessource
								{
									Nom = "A venir",
									Adresse = "-",
									Telephone = "-",
									URL = "-",
									Description = "-"
								}
							},
						},
					},
				}
			
			},
		};

		public void OnQuebecClicked(object sender, EventArgs e)
		{
			bottomListView.ItemsSource = null;
			UpdateResourcesListView("Quebec");
		}

		public void OnMontrealClicked(object sender, EventArgs e)
		{
			bottomListView.ItemsSource = null;
			UpdateResourcesListView("Montreal");
		}

		public void OnOutaouaisClicked(object sender, EventArgs e)
		{
			bottomListView.ItemsSource = null;
			UpdateResourcesListView("Outaouais");
		}

		public void OnOtherClicked(object sender, EventArgs e)
		{
			bottomListView.ItemsSource = null;
			UpdateResourcesListView("Other");
		}

		public void OnResourceSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem != null)
			{
				var resourceList = (ListeRessources)e.SelectedItem;

				// Convert your Dictionary to a List
				var optionsList = new List<OneRessource>(resourceList.Options.Values);

				// Bind your ListView to this new List
				bottomListView.ItemsSource = optionsList;
			}
		}

		private async void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			try
			{
				var resource = (OneRessource)e.Item;
				await Navigation.PushAsync(new OneRessourceDetailPage(resource));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		public void UpdateResourcesListView(string city)
		{
			if (cityResources.ContainsKey(city))
			{
				var resources = cityResources[city];

				// Mettez à jour la ListView
				resourcesListView.ItemsSource = resources;
			}
		}
	}
}
