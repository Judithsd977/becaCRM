// --------------------------------------------------------------------------------------------------------------------
// <copyright file="P001EntityNameHandler.cs" company="Company Name">
//   Company Name
// </copyright>
// <summary>
//   Plugin for EntityName entity
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Plugins.AppName.Handler
{
    using FactoryDataLayer.Interfaces;
    using Library.Auxiliary.Constants.Entities.Custom;
    using Microsoft.Xrm.Sdk;
    using E = Library.Auxiliary.Constants.Entities.AccountE;


    public class P001AccountHandler
    {
        #region Private Attributes

        private ITracingService _tracingService;
        private IDataFactory _df;

        #endregion Private Attributes

        public P001AccountHandler(IDataFactory df, ITracingService tracingService)
        {
            _df = df;
            _tracingService = tracingService;
        }

        public void PostOperationCreate(Entity preImageRecordE, Entity recordE, Entity postImageRecordE)
        {
            // Salta el trigger, tengo el contexto y he validado. Se que estoy en fase(postOperation)-mensaje(create) de la tabla "X" -> Aplico lógica de negocio para mi proceso

            this._tracingService.Trace("Se ha iniciado el plugin de account en PostOperation - Lab 1");

            if (recordE.LogicalName != E.CF)
            {
                this._tracingService.Trace("La entidad no es account. Saliendo.");
                return;
            }

            Entity accountToUpdate = new Entity(E.CF, recordE.Id);
            bool requiereActualizacion = false;

            // Campo Credit Limit 
            if (!recordE.Contains(E.CreditLlimit))
            {
                accountToUpdate[E.CreditLlimit] = new Money(10000m);
                requiereActualizacion = true;
                this._tracingService.Trace("Preparado Credit Limit por defecto: 10,000.00");
            }

            // Campo Credit Hold 
            if (!recordE.Contains(E.CreditHold))
            {
                accountToUpdate[E.CreditHold] = true;
                requiereActualizacion = true;
                this._tracingService.Trace("Preparado Credit Hold por defecto: Yes");
            }

            // Campo Payment Terms 
            if (!recordE.Contains(E.PaymentTerms))
            {
                accountToUpdate[E.PaymentTerms] = new OptionSetValue(4);
                requiereActualizacion = true;
                this._tracingService.Trace("Preparado Payment Terms por defecto: Net 60");
            }

            if (requiereActualizacion)
            {
                this._tracingService.Trace("Ejecutando Update en base de datos desde PostOperation...");
                _df.Update(accountToUpdate);
            }

            this._tracingService.Trace("Fin de la ejecución del plugin de account en PostOperation - Lab 1");


        }

    }
}
