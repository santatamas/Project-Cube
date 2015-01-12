//
//  ZZZipTests.h
//  zipzap
//
//  Created by Glen Low on 18/10/12.
//
//

#import <XCTest/XCTest.h>

@interface ZZZipTests : XCTestCase

- (void)setUp;
- (void)tearDown;

- (NSURL*)zipFileURL;
- (NSError*)someError;
- (NSData*)dataAtFilePath:(NSString*)filePath;
- (NSArray*)recordsForZipEntries:(NSArray*)zipEntries;
- (void)checkZipEntryRecords:(NSArray*)newEntryRecords
				checkerBlock:(NSData*(^)(NSString* fileName))checkerBlock;

@end
