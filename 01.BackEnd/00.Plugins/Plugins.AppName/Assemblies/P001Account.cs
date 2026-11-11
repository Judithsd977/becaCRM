// --------------------------------------------------------------------------------------------------------------------
// <copyright file="P001EntityName.cs" company="Company Name">
//   Company Name
// </copyright>
// <summary>
//   Plugin for EntityName entity
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Plugins.AppName.Assemblies
{
    using System;
    using FactoryDataLayer;
    using FactoryDataLayer.Interfaces;
    using Microsoft.Xrm.Sdk;
    using Plugins.AppName.Handler;
    using C = Library.Auxiliary.Constants;

    public class P001Account : AccountPreCreate
    {
        public P001Account()
        {
            RegisteredEvents.Add(new Tuple<int, string, string, Action<LocalPluginContext>>(
                C.Process.DynamicsProcess.PluginStage.PostOperation, C.Process.DynamicsProcess.PluginMessage.Create, "account", PostOperationCreate));
        }

        protected void PostOperationCreate(LocalPluginContext localContext)
        {
            localContext.TracingService.Trace("PostOperationCreate of EntityName");

            #region Validations

            if (localContext == null)
            {
                throw new ArgumentNullException(C.Process.DynamicsProcess.LocalSystem);
            }

            if (!this.HasTarget(localContext))
            {
                localContext.TracingService.Trace("Doesn't have target");
                return;
            }

            #endregion Validations

            #region Input parameters

            Entity recordE = localContext.PluginExecutionContext.InputParameters["Target"] as Entity;
            if (recordE == null || recordE.LogicalName != "account")
            {
                localContext.TracingService.Trace("Target no es una entidad account. Saliendo.");
                return;
            }

            // En Create/PostOperation no existe pre-image por defecto y las imágenes
            // no son necesarias para esta lógica. No acceder a las colecciones de
            // imágenes evita KeyNotFoundException si no se registraron.
            Entity preImageRecordE = null;
            Entity postImageRecordE = null;

            #endregion  Input parameters

            IDataFactory df = new DataFactory(localContext.OrganizationService, localContext.TracingService);

            new P001AccountHandler(df, localContext.TracingService).PostOperationCreate(preImageRecordE, recordE, postImageRecordE);
        }

    }
}
