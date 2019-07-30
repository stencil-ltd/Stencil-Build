//
//  VersionCodes.m
//  Unity-iPhone
//
//  Created by Aaron Sarazan on 7/21/18.
//

#include "VersionCodes.h"
#include <stdlib.h>

int unityGetVersionCode() {
    NSString *buildVersion = [[[NSBundle mainBundle] infoDictionary] objectForKey:@"CFBundleVersion"];
    NSInteger asInt = [buildVersion integerValue]; 
    NSLog(@"Unity Bundle Version: %d", asInt);
    return (int) asInt;
}
