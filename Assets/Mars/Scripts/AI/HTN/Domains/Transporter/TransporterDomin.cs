using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PLu.Mars.AI.HTN.Kernel;
 
namespace PLu.Mars.AI.HTN.Domains.Transporter
{

    [CreateAssetMenu(fileName = "TransporterDomain", menuName = "AI/Domain/Transporter")]
    public class TransporterDomin : DomainDefinition<TransporterContext>
    {
        public override FluidHTN.Domain<TransporterContext> Create()
        {
            DomainBuilder2<TransporterContext> builder = new("Transporter");

            FluidHTN.Domain<TransporterContext> domain = builder.Build();





            return domain;

            //return new DomainBuilder2<TransporterContext>("Transporter")
                // .Build() as Domain<TransporterContext>;
                // .Select("Transport")
                //     .Sequence("Pickup")
                //         //.HasState(TransporterWorldState.HasPickupLocation)
                //         //.MoveTo(TransporterWorldState.PickupLocation)
                //         //.Wait(2f)
                //         //.Pickup()
                //     .End()
                //     .Sequence("Dropoff")
                //         //.HasState(TransporterWorldState.HasDropoffLocation)
                //         //.MoveTo(TransporterWorldState.DropoffLocation)
                //         //.Wait(2f)
                //         //.Dropoff()
                //     .End()
                // .End()
                // .Build() as Domain<TransporterContext>;
        }
    }
}

/*
namespace MyWorld
{
    [CreateAssetMenu(fileName = "HumanDomain", menuName = "MyAI/Domain/Human")]
    public class HumanDomainDefinition : AIDomainDefinition
    {
        public override Domain<AIContext> Create()
        {
            return new AIDomainBuilder("Human")
                .Select("Collect Resources")
                    .Sequence("Make Recipe")
                        .HasState(AIWorldState.HasIngredient, 3)
                        .FindWorkshop(WorkshopType.Smithy)
                        .MoveToWorkshop()
                        .Wait(2f)
                        .MakeRecipe(AIResourceType.Axe)

                    .End()  
                    .Sequence("Get Wood")
                        //.Condition("Hasn't wood", (ctx) => !ctx.HasState())
                        .HasFlagState(AIWorldState.HasIngredient, 0, false)
                        .HasState(AIWorldState.HasResourceInSight)
                        .FindResource(AIResourceType.Wood)
                        .MoveToResource()
                        .Wait(2f)
                        .GatherResource(AIResourceType.Wood, 0.5f, 0)
                    .End()
                    .Sequence("Get Iron Ore")
                        .HasFlagState(AIWorldState.HasIngredient, 1, false)
                        .HasState(AIWorldState.HasResourceInSight)
                        .FindResource(AIResourceType.IronOre)
                        .MoveToResource()
                        .Wait(2f)
                        .GatherResource(AIResourceType.IronOre, 0.5f, 1)
                    .End()
              
                .End()
                .Build();
        }
    }
}
*/