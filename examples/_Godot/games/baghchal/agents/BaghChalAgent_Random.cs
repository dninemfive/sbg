﻿using d9.utl;

namespace d9.sbg.examples.baghchal;
public class BaghChalAgent_Random : IBaghChalAgent
{
    public BaghChalAction SelectAction(IEnumerable<BaghChalAction> possibleActions)
        => possibleActions.RandomElement();
}
