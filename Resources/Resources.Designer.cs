﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Il codice è stato generato da uno strumento.
//     Versione runtime:4.0.30319.42000
//
//     Le modifiche apportate a questo file possono provocare un comportamento non corretto e andranno perse se
//     il codice viene rigenerato.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WordleWPF.Resources {
    using System;
    
    
    /// <summary>
    ///   Classe di risorse fortemente tipizzata per la ricerca di stringhe localizzate e così via.
    /// </summary>
    // Questa classe è stata generata automaticamente dalla classe StronglyTypedResourceBuilder.
    // tramite uno strumento quale ResGen o Visual Studio.
    // Per aggiungere o rimuovere un membro, modificare il file con estensione ResX ed eseguire nuovamente ResGen
    // con l'opzione /str oppure ricompilare il progetto VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Restituisce l'istanza di ResourceManager nella cache utilizzata da questa classe.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WordleWPF.Resources.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Esegue l'override della proprietà CurrentUICulture del thread corrente per tutte le
        ///   ricerche di risorse eseguite utilizzando questa classe di risorse fortemente tipizzata.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Cerca una stringa localizzata simile a Informazioni.
        /// </summary>
        public static string InfoLabel {
            get {
                return ResourceManager.GetString("InfoLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Cerca una stringa localizzata simile a Lettere non presenti.
        /// </summary>
        public static string MissingPosChar {
            get {
                return ResourceManager.GetString("MissingPosChar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Cerca una stringa localizzata simile a No.
        /// </summary>
        public static string NoButton {
            get {
                return ResourceManager.GetString("NoButton", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Cerca una stringa localizzata simile a Punteggio.
        /// </summary>
        public static string PointLabel {
            get {
                return ResourceManager.GetString("PointLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Cerca una stringa localizzata simile a Riavvia.
        /// </summary>
        public static string RestartLabel {
            get {
                return ResourceManager.GetString("RestartLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Cerca una stringa localizzata simile a ».
        /// </summary>
        public static string SendArrowLabel {
            get {
                return ResourceManager.GetString("SendArrowLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Cerca una stringa localizzata simile a Tempo di gioco.
        /// </summary>
        public static string StopwatchLabel {
            get {
                return ResourceManager.GetString("StopwatchLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Cerca una stringa localizzata simile a Lunghezza Parola.
        /// </summary>
        public static string WordLengthLabel {
            get {
                return ResourceManager.GetString("WordLengthLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Cerca una stringa localizzata simile a Lettere Presenti.
        /// </summary>
        public static string WrongPosChar {
            get {
                return ResourceManager.GetString("WrongPosChar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Cerca una stringa localizzata simile a Si.
        /// </summary>
        public static string YesButton {
            get {
                return ResourceManager.GetString("YesButton", resourceCulture);
            }
        }
    }
}
